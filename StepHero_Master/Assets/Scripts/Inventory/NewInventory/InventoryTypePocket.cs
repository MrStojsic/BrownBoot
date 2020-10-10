using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryTypePocket
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

    public ItemType pocketsItemType;

    [SerializeField]
    public List<InventoryItem> storedItems;

    public void Initialise(ItemType itemType)
    {
        storedItems = new List<InventoryItem>();
        pocketsItemType = itemType;
    }


    public InventoryItem FindItem(Item item)
    {
        if (item.ItemType == pocketsItemType)
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

    // This is something the shop 
    public int MaxNumberOfItemTransferableFromSource(InventoryItem sourceInventoryItem)
    {
        if (sourceInventoryItem.Item.ItemType == pocketsItemType)
        {
            InventoryItem II = FindItem(sourceInventoryItem.Item);

            if (II != null)
            {
                if (II.NumberOfItem + sourceInventoryItem.NumberOfItem <= II.Item.StackSize)
                { return sourceInventoryItem.NumberOfItem; }

                return II.Item.StackSize - II.NumberOfItem;
            }
            if (!IsFull)
            {
                return sourceInventoryItem.NumberOfItem;
            }
        }
        return 0;
    }

    public bool AttemptTransferItems(InventoryItem sourceInventoryItem, int amountToTransfer)
    {
        if (sourceInventoryItem.Item.ItemType == pocketsItemType && amountToTransfer > 0)
        {
            InventoryItem II = FindItem(sourceInventoryItem.Item);
            if (II != null)
            {
                return TransferToExistingStack(sourceInventoryItem, II, amountToTransfer);
            }
            return TransferToNewStack(sourceInventoryItem, amountToTransfer);
        }
        return false;
    }

    private bool TransferToExistingStack(InventoryItem sourceInventoryItem, InventoryItem inventoryItemDestination, int amountToTransfer)
    {
        if (inventoryItemDestination != null)
        {
            return inventoryItemDestination.AddFromInventoryItem(sourceInventoryItem, amountToTransfer);
        }
        return false;
    }
  
    // TODO ; Ensure that this removes the number of items added from the source items inventory.
    private bool TransferToNewStack(InventoryItem sourceInventoryItem, int amountToTransfer)
    {
        if (!IsFull)
        {
            InventoryItem II = new InventoryItem(sourceInventoryItem.Item, 0);
            II.AddFromInventoryItem(sourceInventoryItem, amountToTransfer);
            storedItems.Add(II);
            return true;
        }
        return false;
    }

    /// <summary>
    /// This skips checking if an InventoryItem is already in the storedItems list,
    /// Primarily for setting up the Pocket when loading or filling an empty shop or loot list.
    /// </summary>
    /// <param name="inventoryItem"></param>
    public void SafeForceAddItem(InventoryItem inventoryItem)
    {
        storedItems.Add(inventoryItem);
    }
    public void SafeForceRemoveItem(InventoryItem inventoryItem)
    {
        storedItems.Remove(inventoryItem);
    }
}
