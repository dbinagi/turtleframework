using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Input
{
    public class CInputKeyboard : CInput
    {
        public override float GetSwipeDeltaY()
        {
            return 0;
        }

        public override bool PressedAnyKey()
        {
            return UnityEngine.Input.anyKeyDown;
        }
    }
}