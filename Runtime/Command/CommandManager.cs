using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Core;
using UnityEditor;
using UnityEngine;
using TurtleGames.Framework.Runtime.Command;
using UnityEngine.UI;
using System;

namespace TurtleGames.Framework.Runtime.Command
{
    public class CommandManager : Singleton<CommandManager>
    {

        [SerializeField]
        [Tooltip("Drag the Canvas where you want to display the Console")]
        Canvas canvasToDisplay;

        [SerializeField]
        [Tooltip("You must create a CommandConfig Scriptable Object and assign it here")]
        SOCommandConfig commandConfig;

        [SerializeField] List<Command> availableCommands = new List<Command>();

        public Canvas CanvasToDisplay { get => canvasToDisplay; set => canvasToDisplay = value; }
        public SOCommandConfig CommandConfig { get => commandConfig; set => commandConfig = value; }
        public List<Command> AvailableCommands { get => availableCommands; set => availableCommands = value; }

        CommandUIHelper uiHelper;

        Stack<ICommand> CommandsStack;

        bool isOpen;

        #region Unity Functions

        protected override void OnAwake()
        {
            uiHelper = new CommandUIHelper();

            // Include default commands
            this.gameObject.AddComponent<HelpCommand>();
        }

        void Update()
        {
            if (Input.GetKeyDown(commandConfig.KeyToOpen))
            {
                if (isOpen)
                {
                    uiHelper.CloseDialog();
                }
                else
                {
                    uiHelper.OpenDialog();
                }
                isOpen = !isOpen;
            }
            if (Input.GetKeyDown(commandConfig.KeyToSubmit))
            {
                Submit();
            }
        }

        #endregion

        #region "Public Functions"

        public void AddCommandAvailable(Command command)
        {
            AvailableCommands.Add(command);
        }

        public void AddCommand(ICommand command, string input)
        {
            CommandsStack.Push(command);
            command.Execute(input);
        }

        public void Undo()
        {
            if (CommandsStack.Count == 0)
                return;

            CommandsStack.Pop().Undo();
        }

        public string Submit(string input)
        {
            string[] lines = input.Split(' ');
            if (lines.Length == 0)
                return "<color='red'>ERROR - Command: '" + input + "' not found.</color>";

            string inputCommand = lines[0];

            if (AvailableCommands == null)
                return "<color='red'>ERROR - No commands available.</color>";

            foreach (ICommand c in AvailableCommands)
            {
                if (c != null)
                {
                    if (c.Pattern() == inputCommand)
                    {
                        return "<color='green'>" + c.Execute(input) + "</color>";
                    }
                }
            }
            return "<color='red'>ERROR - Command: '" + input + "' not found.</color>";
        }

        #endregion

        #region "Private Functions"

        void Submit()
        {
            string output = Submit(uiHelper.GetInputText());

            uiHelper.AddLineToConsoleText(output);

            uiHelper.PrepareInputForTyping();
        }

        #endregion

    }

}