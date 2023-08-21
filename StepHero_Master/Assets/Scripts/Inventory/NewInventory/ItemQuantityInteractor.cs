using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantityInteractor : MonoBehaviour
{
    // DATA.
    private int maxNumberOfItem = 0;
    private int currentNumberOfItem = 0;

    [SerializeField] private DisplayItemDetail displayItemDetail = null;

    [SerializeField] private GameObject pricePanel = null;

    // UI.
    [SerializeField] private Text quantityText = null;

    [SerializeField] private Text priceText = null;
    private int totalPrice;
    private int pricePerItemAfterTax = default;

    [SerializeField] private Text confirmationText = null;

    public delegate void ItemInteraction();
    public ItemInteraction itemInteraction;

    public enum QuantityLimitReason
    {
        NOT_ENOUGH_SPACE,
        NOT_ENOUGH_GOLD,
        NOT_ENOUGH_STOCK,
    }
    private QuantityLimitReason quantityLimitReason;


    public void SetUp(bool doEnable = true)
    {
        switch ((int)displayItemDetail.InteractionType)
        {
            case 0: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                maxNumberOfItem = PlayerInventory.Instance.GetMaxNumberOfItemPurchaseable(displayItemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Buy how many?";
                CalculateFinalPricePerItem();
                UpdatePriceUi(true);
                itemInteraction = PlayerBuyItem;
                break;

            case 1: // PLAYER_SELL, check number of item and number shop can afford.
                maxNumberOfItem = InventoryUiManager.Instance.NonPlayerInventory.GetMaxNumberOfItemPurchaseable(displayItemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Sell how many?";
                CalculateFinalPricePerItem();
                UpdatePriceUi(true);
                itemInteraction = PlayerSellItem;
                break;
            case 2: // PLAYER_USE / DROP, check number of item.
                maxNumberOfItem = displayItemDetail.InventoryItem.NumberOfItem;
                currentNumberOfItem = 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Discard how many?";
                UpdatePriceUi(false);
                itemInteraction = DropItem;
                break;
            case 3: // LOOT, check player inventory.
                maxNumberOfItem = PlayerInventory.Instance.GetMaxNumberOfItemReceivable(displayItemDetail.InventoryItem);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = maxNumberOfItem == 0 ? "FULL" : currentNumberOfItem.ToString();
                confirmationText.text = "Take how many?";
                UpdatePriceUi(false);
                itemInteraction = TakeItem;
                break;
            default:
                break;
        }

        if (doEnable)
        {
            ToggleDisplay(doEnable);
        }
    }

    public void ChooseDesiredQuantity (bool isIncrement)
    {
        if (isIncrement)
        {
            if (currentNumberOfItem < maxNumberOfItem)
            {
                currentNumberOfItem++;
                quantityText.text = currentNumberOfItem.ToString();
                UpdatePriceUi((int)displayItemDetail.InteractionType < 2);
                return;
            }
            Debug.Log(quantityLimitReason);

        }
        else
        {
            if (currentNumberOfItem > 0)
            {
                currentNumberOfItem--;
                quantityText.text = currentNumberOfItem.ToString();
                UpdatePriceUi((int)displayItemDetail.InteractionType < 2);
            }
            // -If we want to allow negative 0 to round back to max, allow below statment.
            /*else
            {
                currentNumberOfItem = maxNumberOfItem;
                quantityText.text = currentNumberOfItem.ToString();
                UpdatePriceUi((int)displayItemDetail.InteractionType < 2);
            }*/
        }
    }

    private void CalculateFinalPricePerItem()
    {
        // if the player is selling we incure a 5% loss to the value, rounted up to the next dollar. so a minimum loss of 1 gold.
        if (displayItemDetail.InteractionType == InteractionType.PLAYER_SELL)
        {
            pricePerItemAfterTax = Mathf.CeilToInt(displayItemDetail.InventoryItem.Item.Price * 0.95f);
        }
        // if the player is buying we pay a 5% surcharge to the value, rounted up to the next dollar. so a minimum surcharge of 1 gold. 
        else
        {
            pricePerItemAfterTax = Mathf.CeilToInt(displayItemDetail.InventoryItem.Item.Price * 1.05f);
        }
    }

    private void UpdatePriceUi(bool doShow = true)
    {
        if (doShow)
        {
            totalPrice = currentNumberOfItem * pricePerItemAfterTax;

            priceText.text = totalPrice.ToString();
        }
        if (pricePanel.activeSelf != doShow)
        {
            pricePanel.SetActive(doShow);
        }
    }

    public void CallAppropriateItemInteractions()
    {
        itemInteraction?.Invoke();
        displayItemDetail.UpdateStackSizeUI();
        ToggleDisplay(false);
    }

    public void DropItem()
    {
        if (currentNumberOfItem > 0)
        {
            PlayerInventory.Instance.RemoveItems(displayItemDetail.InventoryItem, currentNumberOfItem);
        }
    }

    public void TakeAllItem()
    {
        currentNumberOfItem = maxNumberOfItem;
        TakeItem();
        ToggleDisplay(false);
    }

    public void TakeItem()
    {
        PlayerInventory.Instance.AttemptAddItem(displayItemDetail.InventoryItem.Item, currentNumberOfItem);
    }

    public void PlayerBuyItem()
    {
        AttemptToPurchaseItems(InventoryUiManager.Instance.NonPlayerInventory, displayItemDetail.InventoryItem);
    }
    public void PlayerSellItem()
    {
        AttemptToPurchaseItems(PlayerInventory.Instance, displayItemDetail.InventoryItem);
    }

    public bool AttemptToPurchaseItems(Inventory sellersInventory, InventoryItem sellersInventoryItem)
    {
        Inventory buyersInventory = sellersInventory is PlayerInventory ? InventoryUiManager.Instance.NonPlayerInventory : PlayerInventory.Instance;

        if (totalPrice <= buyersInventory.wallet.Gold && sellersInventoryItem.NumberOfItem >= currentNumberOfItem)
        {
            if (buyersInventory.AttemptAddItem(sellersInventoryItem.Item, currentNumberOfItem))
            {
                buyersInventory.wallet.PayOtherWallet(ref sellersInventory.wallet, totalPrice);
                sellersInventoryItem.RemoveItems(currentNumberOfItem);

                print("Buyers gold" + buyersInventory.wallet.Gold + " : Sellers Gold" + sellersInventory.wallet.Gold);
                return true;
            }
        }
        return false;
    }

    public void ToggleDisplay(bool toggleEnable)
    {
        if (gameObject.activeSelf != toggleEnable)
        {
            gameObject.SetActive(toggleEnable);
            displayItemDetail.SetLongOrShortDescription(true);
        }
    }
}