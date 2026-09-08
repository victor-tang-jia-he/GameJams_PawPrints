using System;
using UnityEngine;

namespace Com.DefaultCompany.LostSoul
{
    public class TriggerInteraction : MonoBehaviour
    {
        #region Script Parameters
        #endregion

        #region Fields
        private GameObject target = null;
        private PlayerInputHandler playerInputHandler;
        #endregion

        #region Properties
        #endregion

        #region Unity Methods
        private void Update()
        {
            if (playerInputHandler == null && (playerInputHandler = PlayerInputHandler.Instance) == null) return; // Lazy getter

            if (playerInputHandler.GetInteractPressed())
            {
                if (target)
                    target.GetComponent<IInteractable>().Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(IInteractable.TAG))
            {
                target = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == target)
            {
                target = null;
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Implementation
        #endregion
    }
}