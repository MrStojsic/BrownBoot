using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MerchantInventoryItem : InventoryItem
{
    public MerchantInventoryItem(Item item, int numberOfItem, InventorySlot inventorySlot = null) : base(item, numberOfItem, inventorySlot = null)
    {
        _item = item;
        InventorySlot = inventorySlot;
        NumberOfItem = numberOfItem;
    }

    public override bool Interact()
    {
        return false;
    }

    public void SetStockNumberOfItem(int newNumberOfItem)
    {
        NumberOfItem = newNumberOfItem;
    }
}
