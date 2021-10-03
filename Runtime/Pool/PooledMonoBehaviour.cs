using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TurtleGames.Framework.Runtime.Pool
{
    public class PooledMonoBehaviour : MonoBehaviour
    {

        public bool isAddressable;

        public int poolSize;

        public event Action<PooledMonoBehaviour> OnReturnToPool;
        public UnityEvent<PooledMonoBehaviour> OnSetActive = new UnityEvent();

        public IPool Pool { get { return PoolManager.GetPool(this); } }

        #region "Unity Functions"

        private void OnDisable() => OnReturnToPool?.Invoke(this);

        #endregion

        #region "Public Functions"

        public T Get<T>(bool enable = true) where T : PooledMonoBehaviour
        {
            var pool = PoolManager.GetPool(this);
            var pooledObject = pool.Get<T>();
            if (enable)
            {
                pooledObject.gameObject.SetActive(true);
                OnSetActive.Invoke(pooledObject);
            }
            return pooledObject;
        }

        public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
        {
            var pooledObject = Get<T>();
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = rotation;
            return pooledObject;
        }

        public void ReturnToPool(float delay)
        {
            StartCoroutine(ReturnToPoolAfterSeconds(delay));
        }

        #endregion

        #region "Private Functions"

        private IEnumerator ReturnToPoolAfterSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        #endregion

    }
}