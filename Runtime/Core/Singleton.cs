using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;
        public static T Instance { get { return _instance; } }

        [Tooltip("Indicates if the object is destroyed when changing scene")]
        [SerializeField]
        public bool PersistInScenes;

        private void Awake()
        {
            // First time
            if (_instance == null)
            {
                OnAwake();

                _instance = this as T;
                if(PersistInScenes)
                    DontDestroyOnLoad(this);
            }
            else
            {
                // Same instance already initiated
                if (_instance == this)
                {
                    // Loaded second time, remove object that awake after first
                    Destroy(this.gameObject);
                }
                else
                {
                    // Awake a different instance
                    Destroy(this.gameObject);
                }
            }
        }

        protected virtual void OnAwake() {

        }

    }
}
