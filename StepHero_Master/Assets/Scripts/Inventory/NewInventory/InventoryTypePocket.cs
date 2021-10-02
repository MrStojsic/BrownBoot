using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  InventoryTypePocket
{
    private int _sizeLimit = 1000;
    /// <summary>
    /// This is the limit of how many different items can be stored in the pocket,
    /// By default this is set to 1000 for Shops, but this should be set to the limit for the player when loaded.
    /// </summary>
    public int SizeLimit
    {
        get
        {
            return _sizeLimit;
        }
        set
        {
            _sizeLimit = value;
        }
    }
    public int Count
    {
        get { return storedItems.Count; }
    }
    public bool IsEmpty
    {
        get { return storedItems.Count == 0; }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || Count < _sizeLimit)
            {
                return false;
            }
            return true;
        }
    }

    private ItemType _pocketsItemType;
    public ItemType PocketsItemType
    {
        get => _pocketsItemType;
    }

    [SerializeField]
    public List<InventoryItem> storedItems;

    public void Initialise(ItemType itemType)
    {
        storedItems = new List<InventoryItem>();
        _pocketsItemType = itemType;
    }

    public InventoryItem FindItem(Item item)
    {
        if (item.ItemType == _pocketsItemType)
        {
            for (int i = 0; i < storedItems.Count; i++)
            {
                if (item == storedItems[i].Item)
                {
                    return storedItems[i];
                }
            }
        }
        return null;
    }


    /// <summary>
    /// This should only be called from the inventory that stores this InventoryTypePocket!
    /// </summary>
    /// <param name="sourceInventoryItem"></param>
    /// <param name="amountToReceive"></param>
    /// <returns></returns>
    public bool AttemptReceiveItems(InventoryItem sourceInventoryItem, int amountToReceive)
    {
        if (sourceInventoryItem.Item.ItemType == _pocketsItemType && amountToReceive > 0)
        {
            if (sourceInventoryItem.Item.StackSize > 1)
            {
                InventoryItem inventoryItem = FindItem(sourceInventoryItem.Item);
                if (inventoryItem != null)
                {
                    return ReceiveInExistingStack(sourceInventoryItem, inventoryItem, amountToReceive);
                }
            }
            return ReceiveInNewStack(sourceInventoryItem, amountToReceive);
        }
        // An error has occured.
        return false;
    }

    private bool ReceiveInExistingStack(InventoryItem sourceInventoryItem, InventoryItem inventoryItemDestination, int amountToReceive)
    {
        if (inventoryItemDestination != null)
        {
            return inventoryItemDestination.ReceiveFromInventoryItem(sourceInventoryItem, amountToReceive);
        }
        return false;
    }

    // TODO ; Ensure that this removes the number of items added from the source items inventory.
    private bool ReceiveInNewStack(InventoryItem sourceInventoryItem, int amountToReceive)
    {
        if (!IsFull)
        {
            InventoryItem inventoryItem = new InventoryItem(sourceInventoryItem.Item, 0, sourceInventoryItem.InventorySlot);
            inventoryItem.ReceiveFromInventoryItem(sourceInventoryItem, amountToReceive);
            storedItems.Add(inventoryItem);
            return true;
        }
        return false;
    }

}