using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurtleGames.Framework.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TurtleGames.Framework.Runtime.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        public Canvas Canvas;

        private Dictionary<string, GameObject> elements = new Dictionary<string, GameObject>();

        private Dictionary<string, TextMeshProUGUI> elementsTextMeshPro = new Dictionary<string, TextMeshProUGUI>();

        #region "Public functions"

        public TextMeshProUGUI GetText(string name)
        {
            return elementsTextMeshPro[name];
        }

        public void SetText(string name, object value, bool alsoActivate = false)
        {
            //var element = FindInCanvas(name);
            var textMeshPro = elementsTextMeshPro[name];

            if (textMeshPro == null)
            {
                // If not TextMeshPro Try Text
                var element = FindInCanvas(name);
                var text = element.GetComponent<Text>();
                if (text == null)
                    throw new Exception("Component has no text or doesn't exist by the name: " + name);

                text.text = value.ToString();
            }
            else
            {
                textMeshPro.SetText(value.ToString());
            }

            if (alsoActivate)
            {
                textMeshPro.gameObject.SetActive(true);
            }
        }

        public GameObject FindInCanvas(string name)
        {
            try
            {
                return elements[name];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Could not find element: " + name);
            }
        }

        public void Hide(string name)
        {
            var element = FindInCanvas(name);
            element.SetActive(false);
        }

        public void Show(string name)
        {
            var element = FindInCanvas(name);
            element.SetActive(true);
        }

        public void FadeOutGroup(string element, float duration, Action callback = null)
        {
            GameObject elementObj = UIManager.Instance.FindInCanvas(element);
            elementObj.SetActive(true);

            CanvasGroup group = elementObj.GetComponent<CanvasGroup>();
            if (group == null)
                throw new Exception("No CanvasGroup found for: " + element);

            FadeGroup(group, 1, 0, duration, callback);
        }

        public void FadeInGroup(string element, float duration, Action callback = null)
        {

            GameObject elementObj = UIManager.Instance.FindInCanvas(element);
            elementObj.SetActive(true);

            CanvasGroup group = elementObj.GetComponent<CanvasGroup>();
            if (group == null)
                throw new Exception("No CanvasGroup found for: " + element);

            FadeGroup(group, 0, 1, duration, callback);
        }

        #endregion

        #region "Private Functions"

        protected override void OnAwake()
        {
            if (Canvas == null)
                throw new Exception("Missing Canvas reference");

            foreach (var element in Canvas.GetComponentsInChildren<Transform>(true))
            {
                if (elements.ContainsKey(element.name))
                {

                    //Debug.LogWarning("Another element is already called: " + element.name);
                }
                else
                {
                    elements.Add(element.name, element.gameObject);

                    // Add to another dictionary to optimize
                    var textMeshPro = element.GetComponent<TextMeshProUGUI>();
                    if (textMeshPro != null)
                    {
                        elementsTextMeshPro.Add(element.name, textMeshPro);
                    }
                }

            }
        }

        void FadeGroup(CanvasGroup group, float from, float to, float duration, Action callback = null)
        {
            group.alpha = from;
            LeanTween.alphaCanvas(group, to, duration).setOnComplete(() =>
            {
                group.gameObject.SetActive(group.alpha == 1);
                if (callback != null)
                    callback();
            });
        }


        #endregion

    }
}
