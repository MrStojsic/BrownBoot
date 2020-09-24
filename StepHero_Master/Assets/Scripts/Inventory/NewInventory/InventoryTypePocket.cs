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

    public void Initialise(int itemType)
    {
        storedItems = new List<InventoryItem>();
        pocketsItemType = (ItemType)itemType;
    }


    public InventoryItem FindItem(Item item)
    {
        if (item.ItemType == pocketsItemType)
        {
            for (int i = 0; i < storedItems.Count; i++)
            {
                if (item == storedItems[i].item)
                {
                    return storedItems[i];
                }
            }
        }
        return null;
    }

    // This is something the shop 
    public int MaxNumberOfItemTransferable(InventoryItem inventoryItem)
    {
        if (inventoryItem.item.ItemType == pocketsItemType)
        {
            InventoryItem II = FindItem(inventoryItem.item);

            if (II != null)
            {
                return II.item.StackSize - II.NumberOfItem;
            }
            if (!IsFull)
            {
                return inventoryItem.NumberOfItem;
            }
        }
        return 0;
    }

    public bool AddItem(InventoryItem inventoryItem, int amountToAdd)
    {
        if (inventoryItem.item.ItemType == pocketsItemType && amountToAdd > 0)
        {
            if (PlaceInExistingStack(inventoryItem, amountToAdd))
            {
                return true;
            }
            //return PlaceInNewStack(inventoryItem);
        }
        return false;

      
    }
    // FIXME needs redoing i think????
    private bool PlaceInExistingStack(InventoryItem inventoryItem, int amountToAdd)
    {
        InventoryItem II = FindItem(inventoryItem.item);
        if (II != null)
        {
            return II.AddFromInventoryItem(inventoryItem, amountToAdd);
        }
        return false;
    }

    private bool PlaceInNewStack(InventoryItem inventoryItem)
    {
        if (!IsFull)
        {
            storedItems.Add(inventoryItem);
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
}
