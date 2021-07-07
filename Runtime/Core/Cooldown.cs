using UnityEngine;
using UnityEngine.Events;

namespace TurtleGames.Framework.Runtime.Core
{
    public class Cooldown : MonoBehaviour
    {

        [SerializeField]
        float periodicity;

        [SerializeField]
        float periodicityVariance;

        [SerializeField]
        bool triggerOnStart;

        [SerializeField]
        public UnityEvent OnExecuteEvent;

        public float Periodicity { get => periodicity; set => periodicity = value; }
        public int RemainingMinutes {get{return Mathf.FloorToInt(timeRemaining / 60);}}
        public int RemainingSeconds {get{return Mathf.FloorToInt(timeRemaining % 60);}}

        float timeRemaining;

        #region "Unity Functions"

        // Start is called before the first frame update
        void Start()
        {
            timeRemaining = triggerOnStart ? 0 : periodicity;
        }

        // Update is called once per frame
        void Update()
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                OnExecuteEvent?.Invoke();
                timeRemaining = periodicity;
            }
        }

        #endregion

        #region "Public Functions"

        #endregion

    }
}