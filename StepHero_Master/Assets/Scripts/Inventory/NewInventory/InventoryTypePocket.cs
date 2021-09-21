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

    private InventoryItem FindItem(Item item)
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

    public int GetMaxNumberOfItemRecivable(InventoryItem sourceInventoryItem)
    {
        if (sourceInventoryItem.Item.ItemType == _pocketsItemType)
        {
            InventoryItem inventoryItem = FindItem(sourceInventoryItem.Item);

            if (inventoryItem != null)
            {
                if (inventoryItem.NumberOfItem + sourceInventoryItem.NumberOfItem <= inventoryItem.Item.StackSize)
                { return sourceInventoryItem.NumberOfItem; }

                return inventoryItem.Item.StackSize - inventoryItem.NumberOfItem;
            }
            if (!IsFull)
            {
                return sourceInventoryItem.NumberOfItem;
            }
        }
        return 0;
    }


    public int GetMaxNumberOfItemsPurchaseable(InventoryItem sourceInventoryItem, Inventory destinationInventory, out ItemQuantityInteractor.QuantityLimitReason quantityLimitReasonOut)
    {
        if (destinationInventory.Gold > 0)
        {
            ItemQuantityInteractor.QuantityLimitReason currentQuantityLimitReason = default;
            int confirmedMax = int.MaxValue;
            int unconfirmedMax = 0;

            unconfirmedMax = Mathf.FloorToInt(destinationInventory.Gold / sourceInventoryItem.Item.Price);
            if (unconfirmedMax < confirmedMax)
            {
                confirmedMax = unconfirmedMax;
                currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_GOLD;
            }
            unconfirmedMax = sourceInventoryItem.NumberOfItem;
            if (unconfirmedMax < confirmedMax)
            {
                confirmedMax = unconfirmedMax;

                currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_STOCK ;
            }
            // If the player is selling to a Vendor ignore the inventory stack size as vendors can carry more than the player.
            if (destinationInventory is Player_InventoryManager)
            {
                unconfirmedMax = destinationInventory.InventoryTypePockets[(int)sourceInventoryItem.Item.ItemType].GetMaxNumberOfItemRecivable(sourceInventoryItem);
                if (unconfirmedMax < confirmedMax)
                {
                    confirmedMax = unconfirmedMax;
                    currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_SPACE;
                }
            }
            quantityLimitReasonOut = currentQuantityLimitReason;
            return confirmedMax;
        }
        quantityLimitReasonOut = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_GOLD;
        return 0;

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
            InventoryItem inventoryItem = FindItem(sourceInventoryItem.Item);
            if (inventoryItem != null)
            {
                return ReceiveInExistingStack(sourceInventoryItem, inventoryItem, amountToReceive);
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