using System.Collections;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Pool
{
    public interface IPool
    {

        void SetPrefab(PooledMonoBehaviour prefab);

        T Get<T>() where T : PooledMonoBehaviour;

        void Grow();

        void PrepareData();

        Transform GetTransform();
    }
}