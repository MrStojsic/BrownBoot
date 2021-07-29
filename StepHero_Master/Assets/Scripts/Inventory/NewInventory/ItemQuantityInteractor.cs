using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantityInteractor : MonoBehaviour
{
    // REFERENCES.

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

    // Start is called before the first frame update
    public void SetUp(bool doEnable = true)
    {
        currentNumberOfItem = 0;


        switch (itemDetail.InventoryTypeAsInt)
        {
            case 0: // PLAYER_USE / DROP, check number of item.
                maxNumberOfItem = itemDetail.InventoryItem.NumberOfItem;
                quantityText.text = currentNumberOfItem.ToString();
                confirmationText.text = "Discard how many?";
                pricePanel.SetActive(false);
                itemInteraction = DropSelectedItem;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.
     
                break;
            case 2: // SHOP_BUY, check both player inventory and number of item and number player can afford.

                break;
            case 3: // LOOT, check player inventory.
                maxNumberOfItem = Player_InventoryManager.Instance.inventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].MaxNumberOfItemTransferableFromSource(itemDetail.InventoryItem);
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
            }
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
        Player_InventoryManager.Instance.inventoryTypePockets[(int)itemDetail.InventoryItem.Item.ItemType].AttemptTransferItems(itemDetail.InventoryItem, currentNumberOfItem);
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