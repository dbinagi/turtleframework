using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Command;

namespace TurtleGames.Framework.Runtime.Command
{


    public class HelpCommand : Command
    {
        public override string Execute(string input)
        {
            string response = "Available Commands: ";
            foreach (Command c in CommandManager.Instance.AvailableCommands)
            {
                response += c.Pattern() + ", ";
            }

            response = response.Substring(0, response.Length - 2);

            return response;
        }

        public override string Pattern()
        {
            return "help";
        }

        public override string Help()
        {
            return "Command: help - List all available commands";
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }

    }
}