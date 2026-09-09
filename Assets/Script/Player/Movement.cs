using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    public float MovementSpeed;
    private Vector2 moveInput;
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0,moveInput.y).normalized;
        characterController.Move(MovementSpeed * Time.fixedDeltaTime * movement);
    }
}
