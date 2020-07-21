using UnityEngine;
using TurtleGames.Framework.Runtime.StateMachine;

namespace TurtleGames.Framework.Runtime.UI
{
    class CStateCanvasGroupDecreaseAlpha : IState
    {
        public void OnEnter(CContext context)
        {

        }

        public void OnExit(CContext context)
        {
            
        }

        public void OnFixedUpdate(CContext context)
        {

        }

        public void OnUpdate(CContext context)
        {
            CStateCanvasGroup canvasGroup = (CStateCanvasGroup)context;
            
            canvasGroup.CanvasGroup.alpha -= Time.deltaTime / canvasGroup.Duration;

            if(canvasGroup.CanvasGroup.alpha <= 0)
            {
                CStateMachinesManager.Inst.RemoveStateMachine(context);
            }
        }
    }
}