using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Pool
{

    public static class PoolManager
    {

        // Dictionary that stores all the available pools
        private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();

        public static Pool GetPool(PooledMonoBehaviour prefab)
        {
            if (pools.ContainsKey(prefab))
                return pools[prefab];

            var pool = new GameObject("Pool-" + prefab.name).AddComponent<Pool>();
            pool.prefab = prefab;

            pools.Add(prefab, pool);

            return pool;
        }

        public static void DeletePool(PooledMonoBehaviour prefab)
        {
            if (pools.ContainsKey(prefab))
                pools.Remove(prefab);
        }

    }
}