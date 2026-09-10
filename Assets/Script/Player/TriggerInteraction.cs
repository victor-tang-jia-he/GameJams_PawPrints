using Com.DefaultCompany.LostSoul;
using System;
using UnityEngine;

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
    private void OnEnable()
    {
        CoreSystem.OnServiceRegistered += SetService;
    }


    private void OnDisable()
    {
        CoreSystem.OnServiceRegistered -= SetService;
    }

    private void Start()
    {
        playerInputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
    }
    private void Update()
    {
        if (target)
        {
            if (playerInputHandler.GetInteractPressed())
            {
                target.GetComponent<IInteractable>().Interact();
            }
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
    private void SetService(Type serviceType)
    {
        if (serviceType == typeof(PlayerInputHandler))
        {
            playerInputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
        }
    }
    #endregion
}