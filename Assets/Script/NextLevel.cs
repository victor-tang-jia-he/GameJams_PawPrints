using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IInteractable
{
    public int nextlevelIndex;
    public InteractableItemInfo objectMissingItemInfo;
    public List<InteractableItemInfo> requiredCollectableObjects;

    // remove later
    private void Update()
    {
        if (PlayerInputHandler.Instance)
        {
            if (PlayerInputHandler.Instance.GetInteractPressed())
            {
                Interact();
            }
        }
    }
    public void Interact()
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
                UIManager.Instance.ChangeScene(nextlevelIndex);
                return;
            }
        }

        if (DialogueManager.Instance != null)
        {
            StartCoroutine(DialogueManager.Instance.CO_EnterDialogue(objectMissingItemInfo));
        }
    }
}
