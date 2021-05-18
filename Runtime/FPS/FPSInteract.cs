using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TurtleGames.Framework.Runtime.FPS
{
    public class FPSInteract : Singleton<FPSInteract>
    {
        [SerializeField]
        private float interactDistance;

        [SerializeField]
        private LayerMask interactLayer;

        [SerializeField]
        private bool showDebugRay = false;

        public static UnityAction<IInteractable> OnObjectInSight;
        public static UnityAction OnObjectOutOfSight;
        public static UnityAction<IInteractable> OnObjectActivate;
        public static UnityAction<IInteractable> OnObjectDeactivate;
        public static UnityAction<IInteractable> OnObjectInteract;

        public float InteractDistance { get => interactDistance; set => interactDistance = value; }
        public IInteractable CurrentInteractable { get => currentInteractable; set => currentInteractable = value; }

        private const int RAYCASTHIT_LENGTH = 10;

        private IInteractable currentInteractable;
        private RaycastHit[] results = new RaycastHit[RAYCASTHIT_LENGTH];


        #region Unity Functions

        void Update()
        {
            if (showDebugRay)
                Debug.DrawRay(this.transform.position, this.transform.forward * InteractDistance, Color.magenta);

            Ray ray = new Ray(this.transform.position, this.transform.forward * InteractDistance);

            CleanResults();

            var result = Physics.RaycastNonAlloc(ray, results, InteractDistance, interactLayer);

            if (result > 0)
            {
                for (int i = results.Length - 1; i >= 0; i--)
                {
                    RaycastHit hit = results[i];

                    if (hit.collider != null)
                    {
                        IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                        ObjectOnSight(interactable);
                        break;
                    }
                }
            }
            else
            {
                if (CurrentInteractable != null)
                {
                    ObjectOutOfSight();
                }
            }
        }

        #endregion

        #region Private Functions

        protected override void OnAwake()
        {

        }


        void CleanResults()
        {
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new RaycastHit();
            }
        }

        void ObjectOnSight(IInteractable obj)
        {
            if (obj != CurrentInteractable)
            {
                CurrentInteractable = obj;
                CurrentInteractable.OnSight();
                OnObjectInSight?.Invoke(obj);
            }
        }

        void ObjectOutOfSight()
        {
            CurrentInteractable.OutOfSight();
            CurrentInteractable = null;
            OnObjectOutOfSight?.Invoke();
        }

        #endregion


    }
}
