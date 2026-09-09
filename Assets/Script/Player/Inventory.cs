using Com.DefaultCompany.LostSoul;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : Manager<Inventory>
{
    private readonly Dictionary<int, InteractableItemInfo> inventory = new();
    [SerializeField] private GameObject inventoryUIGrid;
    [SerializeField] private GameObject inventoryItemSlotPrefabs;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void AddItemIntoInventory(InteractableItemInfo itemInfo)
    {
        if (!inventory.TryAdd(itemInfo.id, itemInfo))
        {
            Debug.LogWarning($"item :{itemInfo.name} of id : {itemInfo.id} has already been added");
        }
        if (inventoryUIGrid != null)
        {
            GameObject itemPrefab = Instantiate(inventoryItemSlotPrefabs, inventoryUIGrid.transform);
            InventorySlot invSlot = itemPrefab.GetComponent<InventorySlot>();
            invSlot.itemInfo = itemInfo;
            invSlot.descriptionText = descriptionText;
        }
    }

    public bool CheckItemInInventory(InteractableItemInfo itemInfo)
    {
        return inventory.ContainsKey(itemInfo.id);
    }
}
