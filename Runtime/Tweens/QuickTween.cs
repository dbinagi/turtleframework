using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Tweens
{


    public class QuickTween : MonoBehaviour
    {

        public float time;

        public Vector3 to;

        public bool animateOnStart;

        int tweenId;

        Vector3 originalPos;

        // Start is called before the first frame update
        void Start()
        {
            originalPos = transform.position;

            if (animateOnStart)
                StartAnimation();
        }

        public void StopAnimation()
        {
            LeanTween.cancel(tweenId);
        }

        public void StartAnimation()
        {
            transform.position = originalPos;
            LeanTween.cancel(tweenId);
            var tween = LeanTween.moveLocal(this.gameObject, to + originalPos, time).setLoopPingPong().setEaseInOutSine();
            tweenId = tween.uniqueId;
        }
    }
}
