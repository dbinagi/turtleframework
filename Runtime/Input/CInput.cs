using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Input
{
    public abstract class CInput : MonoBehaviour
    {
        
        private bool blockInput = false;

        /// <summary>
        /// List of all the buttons to be controlled inside the game
        /// </summary>
        protected List<CInputGameButton> gameButtons = new List<CInputGameButton>();

        /// <summary>
        /// Check if button is pressed
        /// </summary>
        /// <param name="name">Name of the button to check</param>
        /// <returns>True: Is pressed, false otherwise.</returns>
        public bool IsPressed(string name)
        {
            CInputGameButton button = gameButtons.Find(x => x.name.Equals(name));
            if (button != null)
            {
                return button.pressed;
            }
            return false;
        }

        public float TimePressed(string name)
        {
            CInputGameButton button = gameButtons.Find(x => x.name.Equals(name));
            if (button != null)
            {
                return button.timePressed;
            }
            return 0.0f;
        }

        public bool IsUp(string name)
        {
            CInputGameButton button = gameButtons.Find(x => x.name.Equals(name));
            if (button != null)
            {
                return button.up;
            }
            return false;
        }

        public bool IsDown(string name)
        {
            CInputGameButton button = gameButtons.Find(x => x.name.Equals(name));
            if (button != null)
            {
                return button.down;
            }
            return false;
        }

        public float GetAxisValue(string name)
        {
            CInputGameButton button = gameButtons.Find(x => x.name.Equals(name));
            if (button != null)
            {
                return UnityEngine.Input.GetAxis(name);
            }
            return 0;
        }

        public void SetBlockInput(bool block)
        {
            blockInput = block;
        }


        public Quaternion MousePositionInScreen(Camera cam, int floorMask, float rayLength)
        {
            Ray camRay = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit floorHit;

            if(Physics.Raycast(camRay, out floorHit, rayLength, floorMask))
            {
                Vector3 mousePos = floorHit.point - transform.position;
                mousePos.y = 0.0f;

                Quaternion newRotation = Quaternion.LookRotation(mousePos, Vector3.up);
                return newRotation;
            }

            return Quaternion.identity;
        }

        public abstract bool PressedAnyKey();

        protected void ParentUpdate()
        {
            UpdateGameButtons();
        }

        protected void UpdateGameButtons()
        {
            if (!blockInput)
            {
                foreach (CInputGameButton button in gameButtons)
                {
                    button.pressed = false;
                    button.down = false;
                    button.up = false;

                    if (UnityEngine.Input.GetButtonDown(button.name))
                    {
                        button.down = true;
                        button.timePressed = 0.0f;
                    }

                    if (UnityEngine.Input.GetButton(button.name))
                    {
                        button.pressed = true;
                        button.timePressed += Time.deltaTime;
                    }

                    if (UnityEngine.Input.GetButtonUp(button.name))
                    {
                        button.up = true;
                    }
                }
            }
        }

        public abstract float GetSwipeDeltaY();

        /*#region "Singleton"

        public static CInput Inst = null;

        void Awake()
        {
            if (Inst == null)
            {
                Inst = this;
            }
            else if (Inst != this)
            {
                Destroy(gameObject);
            }
        }

        #endregion*/
        /*
        #region "Observer for Move"

        private List<IObserverMove> _notifyObservers = new List<IObserverMove>();

        public void addObserver(IObserverMove observer)
        {
            this._notifyObservers.Add(observer);
        }

        public void removeObserver(IObserverMove observer)
        {
            foreach (IObserverMove o in _notifyObservers)
            {
                if (o.Equals(observer))
                {
                    _notifyObservers.Remove(o);
                }
            }
        }

        public void notifyObservers()
        {
            foreach (IObserverMove o in _notifyObservers)
            {
                o.notify(this);
            }
        }

        #endregion
    */
    }

}