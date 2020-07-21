using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Input
{
    public class CInputMouse : CInput
    {

        Vector3 startPosMouse;
        private bool isPressingButton = false;
        float deltaPosY;

        public override float GetSwipeDeltaY()
        {
            if (isPressingButton)
            {
                //StartMoving();

                return deltaPosY;
            }
            return 0;
        }

        public override bool PressedAnyKey()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        private void OnMouseDown()
        {
            startPosMouse = UnityEngine.Input.mousePosition;
            isPressingButton = true;
            deltaPosY = 0;
        }

        private void OnMousePressing()
        {
            deltaPosY = UnityEngine.Input.mousePosition.y - startPosMouse.y;
            startPosMouse = UnityEngine.Input.mousePosition;
        }

        private void OnMouseUp()
        {
            startPosMouse = Vector3.zero;
            isPressingButton = false;
            deltaPosY = 0;
        }

        private void Update()
        {
            ParentUpdate();

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            } else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnMouseUp();
            }

            if (isPressingButton)
            {
                OnMousePressing();
            }
        }

    }
}