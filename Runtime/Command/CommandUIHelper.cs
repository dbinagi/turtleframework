using System;
using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Command;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TurtleGames.Framework.Runtime.Command
{


    public class CommandUIHelper
    {

        GameObject dialog;

        Text textGO;

        Text consoleOutput;


        /*public CommandUIHelper()
        {

        }*/

        #region "Public Functions"

        public void CloseDialog()
        {
            dialog.SetActive(false);
        }

        public void OpenDialog()
        {
            if (dialog == null)
            {
                CreatesDialog();
            }
            dialog.SetActive(true);

            PrepareInputForTyping();
        }


        public string GetInputText()
        {
            return textGO.text;
        }

        public void PrepareInputForTyping(){
            InputField inputField = dialog.GetComponent<InputField>();
            inputField.Select();
            inputField.ActivateInputField();
            inputField.text = "";
        }

        public void AddLineToConsoleText(string line)
        {
            string previousText = consoleOutput.text;

            string[] previousLines = consoleOutput.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string newText = line + Environment.NewLine;

            for (int i = 0; i < CommandManager.Instance.CommandConfig.MaxConsoleOutputLines - 1; i++)
            {
                if (i < previousLines.Length)
                {
                    newText += previousLines[i] + Environment.NewLine;
                }
            }

            consoleOutput.text = newText;
        }

        #endregion

        #region "Private Functions"

        void CreatesDialog()
        {
            // Creates the parent GameObject
            GameObject go = new GameObject(CommandManager.Instance.CommandConfig.GameObjectName);
            go.transform.SetParent(CommandManager.Instance.CanvasToDisplay.transform);
            dialog = go;

            // Add Image for background
            Image img = go.AddComponent<Image>();

            // Add Input Field component
            InputField inputField = go.AddComponent<InputField>();

            inputField.targetGraphic = img;

            // Configure position fixed top
            RectTransform position = go.GetComponent<RectTransform>();

            position.localScale = new Vector3(1, 1, 1);
            position.anchorMin = new Vector2(0, 1);
            position.anchorMax = new Vector2(1, 1);
            position.pivot = new Vector2(0.5f, 1);
            position.offsetMin = new Vector2(0, 0);
            position.offsetMax = new Vector2(0, CommandManager.Instance.CommandConfig.Height);
            position.transform.position = new Vector3(0, 0, 0);
            position.position = new Vector3(0, 0, 0);
            position.anchoredPosition = new Vector3(0, 0, 0);

            // Add Text component
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.SetParent(go.transform);
            text.GetComponent<Text>().color = CommandManager.Instance.CommandConfig.Color;
            text.GetComponent<Text>().font = CommandManager.Instance.CommandConfig.Font;

            inputField.textComponent = text.GetComponent<Text>();

            // Configure position stretch all
            RectTransform positionText = text.GetComponent<RectTransform>();

            positionText.localScale = new Vector3(1, 1, 1);
            positionText.anchorMin = new Vector2(0, 0);
            positionText.anchorMax = new Vector2(1, 1);
            positionText.pivot = new Vector2(0, 1);
            positionText.offsetMin = new Vector2(0, 0);
            positionText.offsetMax = new Vector2(0, 0);
            positionText.transform.position = new Vector3(0, 0, 0);
            positionText.position = new Vector3(0, 0, 0);
            positionText.anchoredPosition = new Vector3(5, -5, 0);

            textGO = text.GetComponent<Text>();

            // Add Console output
            GameObject consoleOutput = new GameObject("Text", typeof(Text));

            consoleOutput.transform.SetParent(go.transform);
            consoleOutput.GetComponent<Text>().color = CommandManager.Instance.CommandConfig.Color;
            consoleOutput.GetComponent<Text>().font = CommandManager.Instance.CommandConfig.Font;

            // Configure position stretch all
            RectTransform positionConsoleOutput = consoleOutput.GetComponent<RectTransform>();

            positionConsoleOutput.localScale = new Vector3(1, 1, 1);
            positionConsoleOutput.anchorMin = new Vector2(0, 0);
            positionConsoleOutput.anchorMax = new Vector2(1, 1);
            positionConsoleOutput.pivot = new Vector2(0, 1);
            positionConsoleOutput.offsetMin = new Vector2(0, 0);
            positionConsoleOutput.offsetMax = new Vector2(0, 0);
            positionConsoleOutput.transform.position = new Vector3(0, 0, 0);
            positionConsoleOutput.position = new Vector3(0, 0, 0);
            positionConsoleOutput.anchoredPosition = new Vector3(5, -20, 0);

            this.consoleOutput = consoleOutput.GetComponent<Text>();
        }

        #endregion

    }
}