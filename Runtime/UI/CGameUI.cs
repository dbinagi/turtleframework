using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurtleGames.Framework.Runtime.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace TurtleGames.Framework.Runtime.UI
{
    public class CGameUI : MonoBehaviour
    {

        public Canvas canvas;

        public List<T> FindInCanvas<T>(string name, bool includeInactive = false)
        {
            List<T> result = new List<T>();
            CGameUI.Inst.canvas.GetComponentsInChildren(includeInactive, result);
            return result;
        }

        #region Handling Text

        public void SetTextMeshPro(string name, string txt)
        {
            List<TextMeshProUGUI> result = FindInCanvas<TextMeshProUGUI>(name);

            TextMeshProUGUI textObj = result.Find(s => s.name == name);

            if (textObj != null)
                textObj.text = txt;
        }

        public void SetText(string name, string txt)
        {
            List<Text> result = FindInCanvas<Text>(name);

            Text textObj = result.Find(s => s.name == name);

            if (textObj != null)
                textObj.text = txt;
        }

        #endregion

        #region Handling Canvas Groups

        public void FadeInCanvasGroup(string group, float time)
        {
            List<CanvasGroup> result = FindInCanvas<CanvasGroup>(name);

            CanvasGroup canvasGroup = result.Find(g => g.name == group);

            if (canvasGroup != null)
            {
                FadeCanvasGroup(canvasGroup, time, true);
            }
        }

        public void FadeOutCanvasGroup(string group, float time)
        {
            List<CanvasGroup> result = FindInCanvas<CanvasGroup>(name);

            CanvasGroup canvasGroup = result.Find(g => g.name == group);
            if (canvasGroup != null)
            {
                FadeCanvasGroup(canvasGroup, time, false);
            }
        }

        private CStateCanvasGroup FindOrCreateStateCanvasGroup(CanvasGroup group, float time)
        {
            CStateCanvasGroup stateMachine = FindStateCanvasGroupByGroup(group);

            if (stateMachine == null)
            {
                stateMachine = new CStateCanvasGroup()
                {
                    CanvasGroup = group,
                    Duration = time
                };

            }
            return stateMachine;
        }

        private void FadeCanvasGroup(CanvasGroup group, float time, bool fadeIn)
        {
            CStateCanvasGroup state = FindOrCreateStateCanvasGroup(group, time);

            if (fadeIn)
            {
                state.SetState(new CStateCanvasGroupIncreaseAlpha());

            }
            else
            {
                state.SetState(new CStateCanvasGroupDecreaseAlpha());
            }

            CStateMachinesManager.Inst.AddStateMachine(state);
        }

        public void StopForCanvasGroup(string group)
        {
            List<CanvasGroup> result = FindInCanvas<CanvasGroup>(name);
            CanvasGroup canvasGroup = result.Find(g => g.name == group);

            List<CContext> ToRemove = new List<CContext>();

            CStateCanvasGroup stateMachine = FindStateCanvasGroupByGroup(canvasGroup);

            foreach (CContext context in ToRemove)
            {
                CStateMachinesManager.Inst.RemoveStateMachine(context);
            }
        }

        private CStateCanvasGroup FindStateCanvasGroupByGroup(CanvasGroup canvasGroup)
        {
            foreach (IContext context in CStateMachinesManager.Inst.Contexts)
            {
                if (context is CStateCanvasGroup)
                {
                    CStateCanvasGroup statecg = (CStateCanvasGroup)context;

                    if (statecg.CanvasGroup == canvasGroup)
                    {
                        return statecg;
                    }
                }
            }
            return null;
        }

        #endregion

        #region Singleton
        public static CGameUI Inst = null;

        private void Awake()
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