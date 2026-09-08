using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CollectableObject : MonoBehaviour, IInteractable
{
    public InteractableItemInfo itemInfo;
    public MeshFilter meshFilter;

    private DialogueManager dialogueManager;
    private PlayerInputHandler playerInputHandler;

    static public string PLAYER_TAG = "Player"; // TODO : Add it to the player


    public void Awake()
    {
        if (itemInfo == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh = itemInfo.mesh;
    }

    private void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }
    private void Update()
    {
        if (playerInputHandler == null && (playerInputHandler = PlayerInputHandler.Instance) == null) return; // Lazy getter
    }
    // Called by the player
    void IInteractable.Interact()
    {
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
