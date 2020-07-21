using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Input
{
    public class CInputMobile : CInput
    {
        public override float GetSwipeDeltaY()
        {
            if (UnityEngine.Input.touches.Length > 0)
            {
                Touch t = UnityEngine.Input.GetTouch(0);

                if (t.phase == TouchPhase.Moved)
                {
                    //StartMoving();

                    return t.deltaPosition.y;
                }
            }
            return 0;
        }

        public override bool PressedAnyKey()
        {
            return UnityEngine.Input.touches.Length > 0;
        }
    }
}