using System;
using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace TurtleGames.Framework.Runtime.Pool
{
    public class AddressablePool : MonoBehaviour, IPool
    {

        // Prefab this Pool is spawning
        public PooledMonoBehaviour prefab;

        // Queue for all instanced GameObjects
        private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

        public Vector3 InstantiatePosition;

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
            PrepareData();
        }

        public void PrepareData()
        {
            LoadAssetAsync(prefab.name);
        }

        public void SetPrefab(PooledMonoBehaviour prefab)
        {
            this.prefab = prefab;
        }

        public Transform GetTransform()
        {
            return this.transform;
        }

        #endregion

        #region "Private Functions"

        void LoadAssetAsync(string path)
        {
            var op = Addressables.LoadAssetAsync<GameObject>(path);
            op.Completed += (operation) =>
            {
                if (operation.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    LoadAssetComplete(operation.Result);
                } else
                {
                    // Manages exception
                }
            };
        }

        void LoadAssetComplete(GameObject pooledObjectObj)
        {
            PooledMonoBehaviour pooledObject = pooledObjectObj.GetComponent<PooledMonoBehaviour>();
            for (int i = 0; i < pooledObject.poolSize; i++)
            {
                InstantiateAsset(pooledObjectObj.name);
            }
        }

        void InstantiateAsset(string path)
        {
            var op = Addressables.InstantiateAsync(path, new InstantiationParameters(InstantiatePosition, Quaternion.identity, null));
            op.Completed += (operation) =>
            {
                if (operation.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                    InstantiateAssetComplete(operation.Result);
            };
        }

        void InstantiateAssetComplete(GameObject pooledObjectObj)
        {
            PooledMonoBehaviour pooledObject = pooledObjectObj.GetComponent<PooledMonoBehaviour>();
            pooledObject.name += " " + (objects.Count);
            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;
            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }

        void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
        {
            pooledObject.transform.SetParent(this.transform);
            objects.Enqueue(pooledObject);
        }

        #endregion

    }
}