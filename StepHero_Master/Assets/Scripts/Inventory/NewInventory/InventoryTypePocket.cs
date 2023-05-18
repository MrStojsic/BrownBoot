using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//- Added ItemCountDelegate and Event from https://www.youtube.com/watch?v=7eOFqwGH_hA&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=64
//  Still want to update the way it works after updatig the player inventory. 
public delegate void ItemCountChanged(Item item);

[System.Serializable]
public class  InventoryTypePocket
{
    public event ItemCountChanged itemCountChanged;

    [SerializeField]
    public List<InventoryItem> storedItems;

    public void Initialise(ItemType itemType)
    {
        storedItems = new List<InventoryItem>();
        _pocketsItemType = itemType;
    }

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

    


    // TODO - This could also be optimised by using item IDs and passing na int ID and comparing it to the storedItems ids.
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
                    return ReceiveInExistingItem(sourceInventoryItem, inventoryItem, amountToReceive);
                }
            }
            return ReceiveInNewItem(sourceInventoryItem, amountToReceive);
        }
        // An error has occured.
        return false;
    }

    private bool ReceiveInExistingItem(InventoryItem sourceInventoryItem, InventoryItem inventoryItemDestination, int amountToReceive)
    {
        if (inventoryItemDestination != null)
        {
            if (inventoryItemDestination.ReceiveFromInventoryItem(sourceInventoryItem, amountToReceive) == true)
            {
                OnItemCountChanged(sourceInventoryItem.Item);
                return true;
            }
           
        }
        return false;
    }

    // TODO ; Ensure that this removes the number of items added from the source items inventory.
    private bool ReceiveInNewItem(InventoryItem sourceInventoryItem, int amountToReceive)
    {
        if (!IsFull)
        {
            InventoryItem inventoryItem = new InventoryItem(sourceInventoryItem.Item, 0, sourceInventoryItem.InventorySlot);
            inventoryItem.ReceiveFromInventoryItem(sourceInventoryItem, amountToReceive);
            storedItems.Add(inventoryItem);
            OnItemCountChanged(sourceInventoryItem.Item);
            return true;
        }
        return false;
    }

    //- WE need to check that the event itemCountChanged has some listeners before we call it otherwise we will get a null ref error.
    public void OnItemCountChanged(Item item)
    {
        if (itemCountChanged != null)
        {
            itemCountChanged.Invoke(item);
        }
    }
}