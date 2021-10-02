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

        public void InitGold(int amountToReceive)
        {
            gold += amountToReceive;
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

  

    public int GetMaxNumberOfItemReceivable(InventoryItem sourceInventoryItem)
    {
            if (sourceInventoryItem.Item.StackSize > 1)
            {
                InventoryItem inventoryItem = _inventoryTypePockets[(int)sourceInventoryItem.Item.ItemType].FindItem(sourceInventoryItem.Item);

                if (inventoryItem != null)
                {
                    if (inventoryItem.NumberOfItem + sourceInventoryItem.NumberOfItem <= inventoryItem.Item.StackSize)
                    { return sourceInventoryItem.NumberOfItem; }

                    return inventoryItem.Item.StackSize - inventoryItem.NumberOfItem;
                }
            }
            if (!_inventoryTypePockets[(int)sourceInventoryItem.Item.ItemType].IsFull)
            {
                return sourceInventoryItem.NumberOfItem;
            }
        
        return 0;
    }

    public int GetMaxNumberOfItemsPurchaseable(InventoryItem sellersInventoryItem, out ItemQuantityInteractor.QuantityLimitReason quantityLimitReasonOut)
    {
        if (wallet.Gold > 0)
        {
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

            // Now we knoe the limiting factor.
            quantityLimitReasonOut = currentQuantityLimitReason;
            return confirmedMax;
        }
        quantityLimitReasonOut = ItemQuantityInteractor.QuantityLimitReason.NOT_ENOUGH_GOLD;
        return 0;

    }

    // This takes in the sellers inventory,

    public bool AttemptToPurchaseItems(Wallet sellersWallet, InventoryItem sellersInventoryItem, int amountToReceive, int pricePerItem)
    {
        int totalPrice = pricePerItem * amountToReceive;
        if (totalPrice <= wallet.Gold && sellersInventoryItem.NumberOfItem >= amountToReceive)
        {
            if (_inventoryTypePockets[(int)sellersInventoryItem.Item.ItemType].AttemptReceiveItems(sellersInventoryItem, amountToReceive))
            {
                wallet.PayOtherWallet(ref sellersWallet, totalPrice);

                print(wallet.Gold + " , " + sellersWallet.Gold);
                return true;
            }
        }
        return false;
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
