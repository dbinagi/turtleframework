using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurtleGames.Framework.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TurtleGames.Framework.Runtime.UI
{
    public class Button3D : MonoBehaviour
    {

        [Header("Event")]

        [SerializeField]
        public UnityEvent OnClick;

        [Header("Configuration")]

        [SerializeField, Tooltip("Distance from camera to Object")]
        public float distanceCheck = 100.0f;

        [SerializeField, Tooltip("Index of GetButtonDown")]
        public int mouseButton = 0;

        [SerializeField, Tooltip("Time to wait before accept a new click")]
        public float minTimeBetweenClicks = 1;

        [SerializeField, Tooltip("If true, won't trigger any other event")]
        public bool disableAfterClick = false;

        UnityEngine.Camera cameraToCheck;
        float timeSinceLastClick;
        bool alreadyClicked = false;

        void Awake(){
            cameraToCheck = UnityEngine.Camera.main;
            timeSinceLastClick = minTimeBetweenClicks;
        }

        void Update()
        {
            timeSinceLastClick += Time.deltaTime;
            if (timeSinceLastClick < minTimeBetweenClicks)
                return;
            if(disableAfterClick && alreadyClicked)
                return;

            if ( Input.GetMouseButtonDown (mouseButton)){
                RaycastHit hit;
                Ray ray = cameraToCheck.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast (ray,out hit, distanceCheck)) {
                    OnClick?.Invoke();
                    timeSinceLastClick = 0;
                    alreadyClicked = true;
                }
            }
        }
    }
}
