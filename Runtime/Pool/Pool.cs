using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Pool
{
    public class Pool : MonoBehaviour, IPool
    {

        // Prefab this Pool is spawning
        public PooledMonoBehaviour prefab;

        // Queue for all instanced GameObjects
        private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

        #region "Unity Functions"

        void OnDestroy()
        {
            PoolManager.DeletePool(prefab);
        }

        #endregion

        #region "Public Functions"

        public T Get<T>() where T : PooledMonoBehaviour
        {
            // If no elements in queue
            if (objects.Count == 0)
                Grow();

            var pooledObject = objects.Dequeue();
            return pooledObject as T;
        }

        public void Grow()
        {
            for (int i = 0; i < prefab.poolSize; i++)
            {
                PooledMonoBehaviour pooledObject = Instantiate(prefab);
                pooledObject.gameObject.name += " " + i;
                pooledObject.OnReturnToPool += AddObjectToAvailableQueue;
                pooledObject.transform.SetParent(this.transform);
                pooledObject.gameObject.SetActive(false);
            }
        }

        public void PrepareData()
        {
            // Nothing
        }

        public Transform GetTransform()
        {
            return this.transform;
        }

        public void SetPrefab(PooledMonoBehaviour prefab)
        {
            this.prefab = prefab;
        }

        #endregion

        #region "Private Functions"

        void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
        {
            pooledObject.transform.SetParent(this.transform);
            objects.Enqueue(pooledObject);
        }

        #endregion

    }
}