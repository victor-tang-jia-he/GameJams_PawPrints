using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : Manager<PlayerInputHandler>
{
    #region Script Parameters
    #endregion

    #region Fields
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool escapePressed = false;
    #endregion

    #region Properties
    public Vector2 MoveDirection { get; private set; } = Vector2.zero;

    public bool InteractPressed
    {
        get
        {
            bool result = interactPressed;
            interactPressed = false;
            return result;
        }
        private set { interactPressed = value; }
    }
    #endregion

    #region Unity Methods
    #endregion

    #region Methods
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }
    }
    public void EscapeKeyPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            escapePressed = true;
        }
        else if (context.canceled)
        {
            escapePressed = false;
        }
    }
    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Submit pressed");
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void LeavePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Application.Quit();
        }
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }

    public bool GetEscapePressed()
    {
        bool result = escapePressed;
        escapePressed = false;
        return result;
    }
    #endregion

    #region Implementation
    #endregion
}