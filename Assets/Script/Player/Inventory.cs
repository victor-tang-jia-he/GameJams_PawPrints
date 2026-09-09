using Com.DefaultCompany.LostSoul;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Manager<Inventory>
{
    private readonly Dictionary<int, InteractableItemInfo> inventory = new();

    public void AddItemIntoInventory(InteractableItemInfo itemInfo)
    {
        if (!inventory.TryAdd(itemInfo.id, itemInfo))
        {
            Debug.LogWarning($"item :{itemInfo.name} of id : {itemInfo.id} has already been added");
        }
    }

    public bool CheckItemInInventory(InteractableItemInfo itemInfo)
    {
        return inventory.ContainsKey(itemInfo.id);
    }
}
