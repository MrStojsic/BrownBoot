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

    // HACK
    // Will likely be replaced with a class to handle gold later.
    [SerializeField] private GameObject pricePanel = null;


    // UI.
    [SerializeField] private Text quantityText = null;

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
                pricePanel.SetActive(false);
                itemInteraction = DropSelectedItem;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.

                maxNumberOfItem = InventoryInteractionManager.Instance.NonPlayerInventory.InventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].GetMaxNumberOfItemsPurchaseable(itemDetail.InventoryItem, InventoryInteractionManager.Instance.NonPlayerInventory, out quantityLimitReason);
                currentNumberOfItem = 1;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Sell how many?";
                pricePanel.SetActive(true);
                // TODO impliment action here.
                //itemInteraction = DropSelectedItem;
                break;
            case 2: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                maxNumberOfItem = InventoryInteractionManager.Instance.PlayerInventory.InventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].GetMaxNumberOfItemsPurchaseable(itemDetail.InventoryItem, InventoryInteractionManager.Instance.PlayerInventory, out quantityLimitReason);
                currentNumberOfItem = 1;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Buy how many?";
                pricePanel.SetActive(true);
                // TODO impliment action here.
                //itemInteraction = DropSelectedItem;
                break;
            case 3: // LOOT, check player inventory.
                maxNumberOfItem = InventoryInteractionManager.Instance.PlayerInventory.InventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].GetMaxNumberOfItemRecivable(itemDetail.InventoryItem);
                currentNumberOfItem = 1;
                quantityText.text = maxNumberOfItem == 0 ? "FULL" : currentNumberOfItem.ToString();
                confirmationText.text = "Take how many?";
                pricePanel.SetActive(true);
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
            }
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
            itemDetail.RemoveItems(currentNumberOfItem);
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
        InventoryInteractionManager.Instance.PlayerInventory.InventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].AttemptTransferItems(itemDetail.InventoryItem, currentNumberOfItem);
    }

    public void BuySelectedItem()
    {

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