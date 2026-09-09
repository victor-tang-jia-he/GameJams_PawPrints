using Com.DefaultCompany.LostSoul;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour, IInteractable
{
    public int nextSceneId;
    public InteractableItemInfo objectMissingItemInfo;
    public List<InteractableItemInfo> requiredCollectableObjects;

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
                CoreSystem.Instance.GetService<UIManager>().ChangeScene(nextSceneId);
                return;
            }
        }

        StartCoroutine(CoreSystem.Instance.GetService<DialogueManager>().CO_EnterDialogue(objectMissingItemInfo));
    }
}
