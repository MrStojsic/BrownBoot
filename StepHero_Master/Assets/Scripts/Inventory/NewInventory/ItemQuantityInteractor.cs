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

    [SerializeField ]private ItemDetail itemDetail;

    // UI.
    [SerializeField] private Text quantityText = null;

    // Start is called before the first frame update
    public void SetUp(int maxNumberOfItem)
    {
        this.maxNumberOfItem = maxNumberOfItem;

        currentNumberOfItem = 0;
        quantityText.text = currentNumberOfItem.ToString();
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

    public void DropQuantityOfSelectedItem()
    {
        if (currentNumberOfItem > 0)
        {
            itemDetail.RemoveItems(currentNumberOfItem);
            ToggleDisplay(false);
        }
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