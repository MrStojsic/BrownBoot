using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantityInteractor : MonoBehaviour
{


    // DATA.
    private int maxNumberOfItem = 0;
    private int currentNumberOfItem = 0;

    [SerializeField] private ItemDetail itemDetail = null;

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
        switch ((int)itemDetail.InteractionType)
        {
            case 0: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                maxNumberOfItem = PlayerInventory.Instance.GetMaxNumberOfItemPurchaseable(itemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Buy how many?";
                CalculateFinalPricePerItem();
                UpdatePriceUi(true);
                itemInteraction = PlayerBuySelectedItem;
                break;

            case 1: // PLAYER_SELL, check number of item and number shop can afford.
                maxNumberOfItem = InventoryPageManager.Instance.NonPlayerInventory.GetMaxNumberOfItemPurchaseable(itemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Sell how many?";
                CalculateFinalPricePerItem();
                UpdatePriceUi(true);
                itemInteraction = PlayerSellSelectedItem;
                break;
            case 2: // PLAYER_USE / DROP, check number of item.
                maxNumberOfItem = itemDetail.InventoryItem.NumberOfItem;
                currentNumberOfItem = 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Discard how many?";
                UpdatePriceUi(false);
                itemInteraction = DropSelectedItem;
                break;
            case 3: // LOOT, check player inventory.
                maxNumberOfItem = PlayerInventory.Instance.GetMaxNumberOfItemReceivable(itemDetail.InventoryItem);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = maxNumberOfItem == 0 ? "FULL" : currentNumberOfItem.ToString();
                confirmationText.text = "Take how many?";
                UpdatePriceUi(false);
                itemInteraction = TakeSelectedItem;
                break;
            default:
                break;
        }

        if (doEnable)
        {
            ToggleDisplay(doEnable);
        }
    }

    // Update is called once per frame
    public void UpdateQuantityPickerValue(bool isIncrement)
    {
        if (isIncrement)
        {
            if (currentNumberOfItem < maxNumberOfItem)
            {
                currentNumberOfItem++;
                quantityText.text = currentNumberOfItem.ToString();
                UpdatePriceUi((int)itemDetail.InteractionType < 2);
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
                UpdatePriceUi((int)itemDetail.InteractionType < 2);
            }
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
            print(doShow);
        }

    }

    private void CalculateFinalPricePerItem()
    {
        // if the player is selling we incure a 5% loss to the value, rounted up to the next dollar. so a minimum loss of 1 gold.
        if (itemDetail.InteractionType == InteractionType.PLAYER_SELL)
        {
            pricePerItemAfterTax = Mathf.CeilToInt(itemDetail.InventoryItem.Item.Price * 0.95f);
        }
        // if the player is buying we pay a 5% surcharge to the value, rounted up to the next dollar. so a minimum surcharge of 1 gold. 
        else
        {
            pricePerItemAfterTax = Mathf.CeilToInt(itemDetail.InventoryItem.Item.Price * 1.05f);
        }
    }

    public void CallAppropriateItemInteractions()
    {
        itemInteraction.Invoke();
        ToggleDisplay(false);
    }

    public void DropSelectedItem()
    {
        if (currentNumberOfItem > 0)
        {
            PlayerInventory.Instance.RemoveItems(itemDetail.InventoryItem, currentNumberOfItem);
        }
    }

    public void TakeAllSelectedItem()
    {
        currentNumberOfItem = maxNumberOfItem;
        TakeSelectedItem();
        ToggleDisplay(false);
    }

    public void TakeSelectedItem()
    {
        PlayerInventory.Instance.AttemptAddItem(itemDetail.InventoryItem.Item, currentNumberOfItem);
    }

    public void PlayerBuySelectedItem()
    {
        AttemptToPurchaseItems(InventoryPageManager.Instance.NonPlayerInventory, itemDetail.InventoryItem);
    }
    public void PlayerSellSelectedItem()
    {
        AttemptToPurchaseItems(PlayerInventory.Instance, itemDetail.InventoryItem);
    }

    public bool AttemptToPurchaseItems(Inventory sellersInventory, InventoryItem sellersInventoryItem)
    {
        Inventory buyersInventory = sellersInventory is PlayerInventory ? InventoryPageManager.Instance.NonPlayerInventory : PlayerInventory.Instance;

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
            itemDetail.ToggleDescriptionVisibility(!toggleEnable);
        }
    }
}