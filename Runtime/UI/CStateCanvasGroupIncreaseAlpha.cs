using UnityEngine;
using TurtleGames.Framework.Runtime.StateMachine;

namespace TurtleGames.Framework.Runtime.UI
{
    class CStateCanvasGroupIncreaseAlpha : IState
    {
        public void OnEnter(CContext context)
        {
            CStateCanvasGroup canvasGroup = (CStateCanvasGroup)context;

            canvasGroup.CanvasGroup.alpha = 0;
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

            canvasGroup.CanvasGroup.alpha += Time.deltaTime / canvasGroup.Duration;

            if(canvasGroup.CanvasGroup.alpha == 1)
            {
                CStateMachinesManager.Inst.RemoveStateMachine(context);
            }
        }
    }
}