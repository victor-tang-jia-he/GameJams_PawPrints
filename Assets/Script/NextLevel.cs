using Com.DefaultCompany.LostSoul;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour, IInteractable
{
    public int nextSceneId;
    public InteractableItemInfo objectMissingItemInfo;
    public List<InteractableItemInfo> requiredCollectableObjects;
    public GameObject interactionPrompt;
    public MenuType menuType = MenuType.Dialogue;

    private void Awake()
    {
        if (requiredCollectableObjects == null || requiredCollectableObjects.Count == 0)
        {
            Debug.LogWarning($"NextLevel: Required Collectable Objects missing");
        }
    }

    void IInteractable.Interact()
    {
        Inventory inventory = CoreSystem.Instance.GetService<Inventory>();
        if (inventory)
        {
            bool isAllRequiredObjGetted = true;
            foreach (InteractableItemInfo obj in requiredCollectableObjects)
            {
                if (!inventory.CheckItemInInventory(obj))
                {
                    isAllRequiredObjGetted = false;
                    break;
                }
            }
            if (isAllRequiredObjGetted)
            {
                CoreSystem.Instance.GetService<UIManager>().ChangeScene(nextSceneId, menuType);
                return;
            }
        }

        StartCoroutine(CoreSystem.Instance.GetService<DialogueManager>().CO_EnterDialogue(objectMissingItemInfo));
    }
}
