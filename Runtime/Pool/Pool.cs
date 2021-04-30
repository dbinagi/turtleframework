using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Pool
{
    public class Pool : MonoBehaviour
    {
        // Prefab this Pool is spawning
        public PooledMonoBehaviour prefab;

        // Queue for all instanced GameObjects
        private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

        #region "Public Functions"

        public T Get<T>() where T : PooledMonoBehaviour
        {
            // If no elements in queue
            if (objects.Count == 0)
                GrowPool();

            var pooledObject = objects.Dequeue();
            return pooledObject as T;
        }

        #endregion

        #region "Private Functions"

        private void GrowPool()
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

        private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
        {
            pooledObject.transform.SetParent(this.transform);
            objects.Enqueue(pooledObject);
        }

        #endregion

    }
}