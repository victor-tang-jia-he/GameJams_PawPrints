using Com.DefaultCompany.LostSoul;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    public float MovementSpeed;
    public float TurnSpeed;
    private PlayerInputHandler inputHandler;

    [Header("Footstep Sound Settings")]
    [SerializeField] private float stepInterval;
    private float footstepTimer;
    [SerializeField] private FMODUnity.EventReference footstepEvent;

    [SerializeField]
    private GameObject playerSkin;

    private void Start()
    {
        inputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y).normalized;
        if (movement != Vector3.zero)
        {
            Vector3 desiredForward = Vector3.RotateTowards(playerSkin.transform.forward, movement, Mathf.Deg2Rad * TurnSpeed * Time.deltaTime, 0.0f);
            playerSkin.transform.rotation = Quaternion.LookRotation(desiredForward);

            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot(footstepEvent);
                footstepTimer = stepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y).normalized;
        characterController.Move(movement * MovementSpeed * Time.fixedDeltaTime);

    }
}
