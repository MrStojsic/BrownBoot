using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantityInteractor : MonoBehaviour
{
   

    // DATA.
    int maxNumberOfItem = 0;
    int currentNumberOfItem = 0;

    [SerializeField ]private ItemDetail itemDetail = null;

    [SerializeField] private GameObject pricePanel = null;




    // UI.
    [SerializeField] private Text quantityText = null;

    [SerializeField] private Text priceText = null;
    private int currentPrice;

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

    // Start is called before the first frame update
    public void SetUp(bool doEnable = true)
    {
        switch ((int)itemDetail.InteractionType)
        {
            case 0: // PLAYER_USE / DROP, check number of item.
                maxNumberOfItem = itemDetail.InventoryItem.NumberOfItem;
                currentNumberOfItem = 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Discard how many?";
                UpdatePriceUi(false);
                itemInteraction = DropSelectedItem;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.

                maxNumberOfItem = InventoryPageManager.Instance.NonPlayerInventory.GetMaxNumberOfItemsPurchaseable(itemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Sell how many?";
                UpdatePriceUi(true);
                itemInteraction = PlayerSellSelectedItem;
                break;
            case 2: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                maxNumberOfItem = InventoryPageManager.Instance.PlayerInventory.GetMaxNumberOfItemsPurchaseable(itemDetail.InventoryItem, out quantityLimitReason);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Buy how many?";
                UpdatePriceUi(true);
                itemInteraction = PlayerBuySelectedItem;
                break;
            case 3: // LOOT, check player inventory.
                maxNumberOfItem = InventoryPageManager.Instance.PlayerInventory.GetMaxNumberOfItemReceivable(itemDetail.InventoryItem);
                currentNumberOfItem = maxNumberOfItem > 0 ? 1 : 0;
                quantityText.text = maxNumberOfItem == 0 ? "FULL" : currentNumberOfItem.ToString();
                confirmationText.text = "Take how many?";
                UpdatePriceUi(true);
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
                UpdatePriceUi();
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
                UpdatePriceUi();
            }
        }
    }

    private void UpdatePriceUi(bool doShow = true)
    {
        if (doShow)
        {
            currentPrice = currentNumberOfItem * itemDetail.InventoryItem.Item.Price;
            priceText.text = currentPrice.ToString();
        }
        if (pricePanel.activeSelf != doShow)
        {
            pricePanel.SetActive(doShow);
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
            itemDetail.InventoryItem.RemoveItems(currentNumberOfItem);
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
        InventoryPageManager.Instance.PlayerInventory.InventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].AttemptReceiveItems(itemDetail.InventoryItem, currentNumberOfItem);
    }

    public void PlayerBuySelectedItem()
    {
        InventoryPageManager.Instance.PlayerInventory.AttemptToPurchaseItems(InventoryPageManager.Instance.NonPlayerInventory.wallet,
                                                                             itemDetail.InventoryItem,
                                                                             currentNumberOfItem,
                                                                             itemDetail.InventoryItem.Item.Price);
    }
    public void PlayerSellSelectedItem()
    {
        InventoryPageManager.Instance.NonPlayerInventory.AttemptToPurchaseItems(InventoryPageManager.Instance.PlayerInventory.wallet,
                                                                                itemDetail.InventoryItem,
                                                                                currentNumberOfItem,
                                                                                itemDetail.InventoryItem.Item.Price);
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