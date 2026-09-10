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

    [Header("Surface Material TAG")]
    [SerializeField] private string grassTag = "Grass";
    [SerializeField] private string rockTag = "Rock";
    [SerializeField] private string snowTag = "Snow";

    string currentSurfaceMaterial = "Grass";

    [SerializeField]
    private GameObject playerSkin;

    public bool IsMoving { get; private set; } = false;

    private void Start()
    {
        inputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y).normalized;
        if (movement != Vector3.zero)
        {
            IsMoving = true;
            Vector3 desiredForward = Vector3.RotateTowards(playerSkin.transform.forward, movement, Mathf.Deg2Rad * TurnSpeed * Time.deltaTime, 0.0f);
            playerSkin.transform.rotation = Quaternion.LookRotation(desiredForward);

            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot(footstepEvent, "Footsteps surface", currentSurfaceMaterial);
                footstepTimer = stepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
            IsMoving = false;
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(inputHandler.MoveDirection.x, 0, inputHandler.MoveDirection.y).normalized;
        characterController.Move(movement * MovementSpeed * Time.fixedDeltaTime);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down * 5f), out hit))
        {
            if (hit.transform.CompareTag(grassTag))
            {
                currentSurfaceMaterial = grassTag;
            }
            if (hit.transform.CompareTag(snowTag))
            {
                currentSurfaceMaterial = snowTag;
            }
            if (hit.transform.CompareTag(rockTag))
            {
                currentSurfaceMaterial = rockTag;
            }
        }
    }
}
