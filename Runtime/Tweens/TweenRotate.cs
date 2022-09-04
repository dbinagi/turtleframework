using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Tweens
{
    public class TweenRotate: MonoBehaviour
    {

        [SerializeField]
        public float RotateSpeed;

        [SerializeField]
        public bool AnimateOnStart;

        [SerializeField]
        public Vector3 Axis;

        bool enableRotation = false;

        void Start(){
            if (AnimateOnStart)
                enableRotation = true;
        }

        void Update(){
            if (!enableRotation)
                return;
            
                this.transform.Rotate(Axis, RotateSpeed * Time.deltaTime);
        }

        [ContextMenu("Start")]
        public void StartRotation(){
            enableRotation = true;
        }

        [ContextMenu("Stop")]
        public void StopRotation(){
            enableRotation = false;
        }
    

    }

}
