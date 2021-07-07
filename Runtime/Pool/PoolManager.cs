using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Pool
{

    public static class PoolManager
    {

        // Dictionary that stores all the available pools
        private static Dictionary<PooledMonoBehaviour, IPool> pools = new Dictionary<PooledMonoBehaviour, IPool>();

        public static IPool GetPool(PooledMonoBehaviour prefab)
        {
            if (pools.ContainsKey(prefab))
                return pools[prefab];

            if (prefab.isAddressable)
                return AddComponent<AddressablePool>(prefab);
            else
            {
                return AddComponent<Pool>(prefab);
            }
        }

        private static IPool AddComponent<T>(PooledMonoBehaviour prefab) where T : Component
        {
            var pool = new GameObject("Pool-" + prefab.name).AddComponent<T>();
            
            IPool poolInterface = pool.GetComponent<IPool>();
            poolInterface.SetPrefab(prefab);
            pools.Add(prefab, poolInterface);

            return poolInterface;
        }

        public static void DeletePool(PooledMonoBehaviour prefab)
        {
            if (pools.ContainsKey(prefab))
                pools.Remove(prefab);
        }

    }
}