using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private InventoryTypePocket[] _inventoryTypePockets = new InventoryTypePocket[14];

    public InventoryTypePocket[] InventoryTypePockets
    {
        get => _inventoryTypePockets;
        protected set => _inventoryTypePockets = value;
    }

    private int gold = 1000;
    [SerializeField]
    public int Gold
    {
        get => gold;
        protected set => gold = value;
    }

    // TODO
    // These were taken from InventoryTypePocket as it was unsafe to have them in ther.

    /// <summary>
    /// This skips checking if an InventoryItem is already in the storedItems list,
    /// this should ONLY be used by player for loading the save data and,
    /// the loot and vendors to fill their inventory when randomly stocked.
    /// </summary>
    /// <param name="inventoryItem"></param>
    public void UnsafeForceAddItem(InventoryItem inventoryItem)
    {
        _inventoryTypePockets[(int)inventoryItem.Item.ItemType].storedItems.Add(inventoryItem);
    }
    public void RemoveEmptyInventoryItemFromPocket(InventoryItem inventoryItem)
    {
        if (inventoryItem.NumberOfItem == 0)
        {
            _inventoryTypePockets[(int)inventoryItem.Item.ItemType].storedItems.Remove(inventoryItem);
        }
    }
}
