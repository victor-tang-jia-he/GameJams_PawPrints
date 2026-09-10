using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InteractableItemInfo itemInfo;
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        if (itemInfo != null && itemInfo.icon != null)
        {
            GetComponent<Image>().sprite = itemInfo.icon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInfo != null && descriptionText != null)
        {
            descriptionText.text = itemInfo.description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null)
        {
            descriptionText.text = string.Empty;
        }
    }

    private void OnDisable()
    {
        if (descriptionText != null)
        {
            descriptionText.text = string.Empty;
        }
    }
}
