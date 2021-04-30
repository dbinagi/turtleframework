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

        #region "Public functions"

        public void SetText(string name, object value)
        {
            var element = FindInCanvas(name);

            var textMeshPro = element.GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                // If not TextMeshPro Try Text
                var text = element.GetComponent<Text>();
                if (text == null)
                    throw new Exception("Component has no text or doesn't exist by the name: " + name);

                text.text = value.ToString();
            }
            else
            {
                textMeshPro.SetText(value.ToString());
            }
        }

        public GameObject FindInCanvas(string name)
        {
            foreach (var element in elements)
            {
                if (element.Key == name)
                    return element.Value;
            }
            throw new Exception("Could not find element: " + name);
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

        #endregion

        #region "Private Functions"

        protected override void OnAwake()
        {
            if (Canvas == null)
                throw new Exception("Missing Canvas reference");

            foreach (var element in Canvas.GetComponentsInChildren<Transform>(true))
            {
                if (elements.ContainsKey(element.name))
                    Debug.LogWarning("Another element is already called: " + element.name);
                else              
                    elements.Add(element.name, element.gameObject);
            }
        }

        #endregion

    }
}