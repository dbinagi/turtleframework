using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Camera
{

    public class SmoothFollow : MonoBehaviour
    {
        public Transform target;

        public float followStrength;

        float targetOffsetX;
        float targetOffsetZ;

        void Start()
        {
            Vector3 originalTargetPos = target.position;

            targetOffsetX = this.transform.position.x - originalTargetPos.x;
            targetOffsetZ = this.transform.position.z - originalTargetPos.z;
        }

        void Update()
        {
            Vector3 newPos = new Vector3(target.transform.position.x + targetOffsetX, this.transform.position.y, target.transform.position.z + targetOffsetZ);
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * followStrength);
        }

    }
}
