using Com.DefaultCompany.LostSoul;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CollectableObject : MonoBehaviour, IInteractable
{
    public InteractableItemInfo itemInfo;
    public MeshFilter meshFilter;

    private bool canInteract = false;

    private DialogueManager dialogueManager;

    static public string PLAYER_TAG = "Player"; // TODO : Add it to the player

    public void SetInteractEnabled(bool enabled)
    {
        canInteract = enabled;
    }
    public void Awake()
    {
        if (itemInfo == null)
        {
            Debug.LogWarning($"Collectable Object Item info missing");
            return;
        }

        meshFilter = GetComponent<MeshFilter>();
        if (itemInfo.mesh != null)
        {
            meshFilter.mesh = itemInfo.mesh;
        }
    }

    private void Start()
    {
        dialogueManager = CoreSystem.Instance.GetService<DialogueManager>();
    }

    void IInteractable.Interact()
    {
        if (!canInteract) return;

        if (dialogueManager != null)
        {
            StartCoroutine(dialogueManager.CO_EnterDialogue(itemInfo));
        }
        dialogueManager.OnDialogueEnded.AddListener(AutoDeleteOnDialogueEnded);
    }
    private void AutoDeleteOnDialogueEnded()
    {
        dialogueManager.OnDialogueEnded.RemoveListener(AutoDeleteOnDialogueEnded);
        CoreSystem.Instance?.GetService<Inventory>().AddItemIntoInventory(itemInfo);
        Destroy(gameObject);
    }

}
