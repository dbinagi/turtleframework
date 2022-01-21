using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Command
{
    public interface ICommand
    {
        string Execute(string input);
        void Undo();
        string Pattern();
        string Help();
    }

}
