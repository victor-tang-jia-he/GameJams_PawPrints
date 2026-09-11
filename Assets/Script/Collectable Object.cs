using Com.DefaultCompany.LostSoul;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IInteractable
{
    public InteractableItemInfo itemInfo;

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
