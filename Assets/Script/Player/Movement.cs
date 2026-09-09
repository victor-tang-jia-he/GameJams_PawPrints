using Com.DefaultCompany.LostSoul;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    public float MovementSpeed;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        inputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y).normalized;
        characterController.Move(movement * MovementSpeed * Time.fixedDeltaTime);
    }
}
