using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CollectableObject : MonoBehaviour, IInteractable
{
    public InteractableItemInfo itemInfo;
    public MeshFilter meshFilter;
    public float showDuration = 3.0f;

    private float showTimer = 0.0f;
    private bool isShowing = false;

    private DialogueManager dialogueManager;
    private PlayerInputHandler playerInputHandler;
    private MeshRenderer meshRenderer;

    static public string PLAYER_TAG = "Player"; // TODO : Add it to the player

    public void GetScanned()
    {
        showTimer = showDuration;
        isShowing = true;
        meshRenderer.enabled = true;
    }


    public void Awake()
    {
        if (itemInfo == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = itemInfo.mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }
    private void Update()
    {
        if (playerInputHandler == null && (playerInputHandler = PlayerInputHandler.Instance) == null) return; // Lazy getter

        if (isShowing)
        {
            showTimer -= Time.deltaTime;
            if (showTimer <= 0.0f)
            {
                isShowing = false;
                showTimer = 0.0f;
                meshRenderer.enabled = false;
            }
        }
    }
    // Called by the player
    void IInteractable.Interact()
    {
        if (!isShowing) return;

        if (dialogueManager != null)
        {
            StartCoroutine(dialogueManager.CO_EnterDialogue(itemInfo));
        }
        DialogueManager.OnDialogueEnded += AutoDeleteOnDialogueEnded;
    }

    private void AutoDeleteOnDialogueEnded()
    {
        DialogueManager.OnDialogueEnded -= AutoDeleteOnDialogueEnded;
        Destroy(gameObject);
    }

}
