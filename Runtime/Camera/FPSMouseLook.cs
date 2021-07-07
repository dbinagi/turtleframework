using UnityEngine;

namespace TurtleGames.Framework.Runtime.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class FPSMouseLook : MonoBehaviour
    {
        [SerializeField]
        private CursorLockMode startLockMode;

        [SerializeField]
        private float mouseSensitivity;

        [Tooltip("Lerp Smooth for the axis X rotation")]
        [SerializeField]
        private float rotationXSmooth;

        [Tooltip("Lerp Smooth for the axis Y rotation")]
        [SerializeField]
        private float rotationYSmooth;

        [Tooltip("Set to True to block all rotations")]
        private bool blockMovement;

        [Tooltip("Limit of the camera when looking at the floor related to the horizon")]
        [SerializeField]
        private float lookAtFloorRotationLimit;

        [Tooltip("Limit of the camera when looking at the ceiling related to the horizon")]
        [SerializeField]
        private float lookAtCeilingRotationLimit;

        public bool BlockMovement { get => blockMovement; set => blockMovement = value; }
        public CursorLockMode StartLockMode { get => startLockMode; set => startLockMode = value; }

        #region Unity Functions

        void Start()
        {
            Cursor.lockState = StartLockMode;
        }

        void Update()
        {
            if (BlockMovement)
                return;

            RotateY();
            RotateX();
        }

        #endregion

        #region Public Functions

        public void FreeMoveMouse()
        {
            BlockMovement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void LockMouse()
        {
            BlockMovement = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #endregion

        #region Private Functions

        void RotateY()
        {
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * -1;
            Quaternion rotation = Quaternion.Euler(mouseY, 0, 0) * transform.localRotation;

            Quaternion newRotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime * rotationYSmooth);

            if (newRotation.eulerAngles.z != 0)
                return;

            bool applyRotation = false;

            // Look down quadrant
            if(newRotation.eulerAngles.x >= 0 && newRotation.eulerAngles.x <= 90)
            {
                if (newRotation.eulerAngles.x <= lookAtFloorRotationLimit)
                    applyRotation = true;
            // Look up quadrant
            } else if(newRotation.eulerAngles.x >= 270 && newRotation.eulerAngles.x <= 360)
            {
                if (newRotation.eulerAngles.x >= 360 - lookAtCeilingRotationLimit)
                    applyRotation = true;
            }

            if (applyRotation)
                transform.localRotation = newRotation;
        }

        void RotateX()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            Quaternion newRotation = Quaternion.AngleAxis(mouseX, Vector3.up) * transform.parent.rotation;
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, newRotation, Time.deltaTime * rotationXSmooth);
        }

        #endregion
    }
}