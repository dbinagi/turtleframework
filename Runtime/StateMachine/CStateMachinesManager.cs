using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.StateMachine
{
    public class CStateMachinesManager : MonoBehaviour
    {

        private List<IContext> contexts = new List<IContext>();

        public List<IContext> Contexts { get => contexts; set => contexts = value; }

        private void FixedUpdate()
        {
            foreach (IContext context in Contexts.ToArray()) {
                context.Update();
            }
        }

        public void AddStateMachine(IContext context)
        {
            if(!contexts.Contains(context))
                Contexts.Add(context);
        }

        public void RemoveStateMachine(IContext context)
        {
            Contexts.Remove(context);
        }

        #region Singleton
        public static CStateMachinesManager Inst = null;

        void Awake()
        {
            if (Inst == null)
            {
                Inst = this;
            }
            else if (Inst != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}