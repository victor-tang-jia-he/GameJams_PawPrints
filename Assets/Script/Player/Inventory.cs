using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private readonly Dictionary<int, InteractableItemInfo> inventory = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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
