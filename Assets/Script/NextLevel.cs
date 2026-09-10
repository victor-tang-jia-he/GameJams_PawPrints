using Com.DefaultCompany.LostSoul;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NextLevel : MonoBehaviour, IInteractable
{
    public int nextSceneId;
    [SerializeField] private InteractableItemInfo objectMissingItemInfo;
    [SerializeField] private List<InteractableItemInfo> requiredCollectableObjects = new();
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private VideoClip videoClipDuringTransition;
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
                CoreSystem.Instance.GetService<UIManager>().ChangeScene(nextSceneId, menuType, videoClipDuringTransition);
                return;
            }
        }

        StartCoroutine(CoreSystem.Instance.GetService<DialogueManager>().CO_EnterDialogue(objectMissingItemInfo));
    }
}
