using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Wallet
    {
        [SerializeField]
        private int gold;

        public int Gold
        {
            get => gold;
        }

        public void InitializeWallet(int initialGoldAmount)
        {
            gold = initialGoldAmount;
        }
        public bool PayOtherWallet(ref Wallet recipientWallet, int amountGiven)
        {
            if (gold >= amountGiven)
            {
                gold -= amountGiven;
                recipientWallet.gold += amountGiven;

                return true;
            }
            return false;
        }
    }
    public Wallet wallet;

    [SerializeField]
    private InventoryTypePocket[] _inventoryTypePockets = new InventoryTypePocket[15];
    public InventoryTypePocket[] InventoryTypePockets
    {
        get => _inventoryTypePockets;
        protected set => _inventoryTypePockets = value;
    }



    // TODO - This could also be optimised by using item IDs and passing na int ID and comparing it to the storedItems ids.
    public InventoryItem FindItem(Item item)
    {
        return _inventoryTypePockets[item.GetItemTypeInt()].FindItem(item);
    }
    public int FindNumerOfItem(Item item)
    {
        return _inventoryTypePockets[item.GetItemTypeInt()].FindNumberOfItem(item);
    }



    /// <summary>
    /// This should only be called from the inventory that stores this InventoryTypePocket!
    /// </summary>
    /// <param name="sourceInventoryItem"></param>
    /// <param name="amountToReceive"></param>
    /// <returns></returns>
    public virtual bool AttemptAddItem(Item item, int amountToReceive)
    {
        if (amountToReceive > 0)
        {
            InventoryItem inventoryItem = FindItem(item);
            if (inventoryItem != null)
            {
                return ReceiveExistingItem(item, inventoryItem, amountToReceive);
            }

            return ReceiveNewItem(item, amountToReceive);
        }
        // An error has occured.
        return false;
    }
    /*    public bool AttemptAddItems(InventoryItem sourceInventoryItem, int amountToReceive)
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
    }*/

    private bool ReceiveExistingItem(Item item, InventoryItem itemDestination, int amountToReceive)
    {
        if (itemDestination != null)
        {
            if (itemDestination.ReceiveItem(item, amountToReceive) == true)
            {
                return true;
            }

        }
        return false;
    }
    /*
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
    }*/

    private bool ReceiveNewItem(Item item, int amountToReceive)
    {
        int pocketIndex = item.GetItemTypeInt();
        if (!_inventoryTypePockets[pocketIndex].IsFull)
        {
            InventoryItem inventoryItem = new InventoryItem(item, amountToReceive);
            _inventoryTypePockets[pocketIndex].storedItems.Add(inventoryItem);
            return true;
        }
        return false;
    }
    /*
   
    private bool ReceiveNewItem(InventoryItem sourceInventoryItem, int amountToReceive)
    {
        if (!IsFull)
        {
            InventoryItem inventoryItem = new InventoryItem(sourceInventoryItem.Item, 0, sourceInventoryItem.InventorySlot);
            inventoryItem.ReceiveItem(sourceInventoryItem.Item, amountToReceive);
            storedItems.Add(inventoryItem);
            OnItemCountChanged(sourceInventoryItem.Item);
            return true;
        }
        return false;
    }
    */

    public int GetMaxNumberOfItemReceivable(InventoryItem sourceInventoryItem)
    {
        if (sourceInventoryItem.NumberOfItem > 0)
        {
            InventoryItem inventoryItem = FindItem(sourceInventoryItem.Item);

            if (inventoryItem != null)
            {
                if (inventoryItem.NumberOfItem + sourceInventoryItem.NumberOfItem <= inventoryItem.Item.StackSize)
                { return sourceInventoryItem.NumberOfItem; }

                return inventoryItem.Item.StackSize - inventoryItem.NumberOfItem;
            }
        }
        if (!_inventoryTypePockets[(int)sourceInventoryItem.Item.GetItemTypeInt()].IsFull)
        {
            return sourceInventoryItem.NumberOfItem;
        }

        return 0;
    }

    public int GetMaxNumberOfItemPurchaseable(InventoryItem sellersInventoryItem, out ItemQuantityInteractor.QuantityLimitReason quantityLimitReasonOut)
    {
        //- If buyers gold is 0 return reason now and 0 purchasable.
        if (wallet.Gold == 0)
        {
            quantityLimitReasonOut = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_GOLD;
            return 0;
        }

        //- Otherwise find out number purchaseable and the limiting reason if any.
        ItemQuantityInteractor.QuantityLimitReason currentQuantityLimitReason = default;
        int confirmedMax = int.MaxValue;
        int unconfirmedMax = 0;

        // Check if the buyers gold will be the purchase limiting factor.
        unconfirmedMax = Mathf.FloorToInt(wallet.Gold / sellersInventoryItem.Item.Price);
        if (unconfirmedMax < confirmedMax)
        {
            confirmedMax = unconfirmedMax;
            currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_GOLD;
        }
        // Check if the sellers stock level will be the purchase limiting factor.
        unconfirmedMax = sellersInventoryItem.NumberOfItem;
        if (unconfirmedMax < confirmedMax)
        {
            confirmedMax = unconfirmedMax;

            currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_STOCK;
        }

        // UNSURE OF FEATURE HERE.
        // If i want the player to be able to sell items over the merchants standard x99 stack size, use the check below
        // "if (buyersInventory is Player_InventoryManager)" before the buyers inventory space check.

        // Check if the buyers inventory space will be the purchase limiting factor.
        unconfirmedMax = GetMaxNumberOfItemReceivable(sellersInventoryItem);
        if (unconfirmedMax < confirmedMax)
        {
            confirmedMax = unconfirmedMax;
            currentQuantityLimitReason = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_SPACE;
        }

        // Now we know the limiting factor.
        quantityLimitReasonOut = currentQuantityLimitReason;
        return confirmedMax;
    }

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
