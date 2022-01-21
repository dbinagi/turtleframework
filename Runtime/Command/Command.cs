using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Command
{
    public abstract class Command : MonoBehaviour, ICommand
    {
        public abstract string Execute(string input);

        public abstract string Help();

        public abstract string Pattern();

        public abstract void Undo();

        private void Start() {
            CommandManager.Instance.AddCommandAvailable(this);
        }

    }

}
