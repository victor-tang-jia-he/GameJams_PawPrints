using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour, IInteractable
{
    public string nextSceneName;
    public InteractableItemInfo objectMissingItemInfo;
    public List<InteractableItemInfo> requiredCollectableObjects;

    void IInteractable.Interact()
    {
        Inventory inventory = Inventory.Instance;
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
                UIManager.Instance.ChangeScene(nextSceneName);
                return;
            }
        }

        if (DialogueManager.Instance != null)
        {
            StartCoroutine(DialogueManager.Instance.CO_EnterDialogue(objectMissingItemInfo));
        }
    }
}
