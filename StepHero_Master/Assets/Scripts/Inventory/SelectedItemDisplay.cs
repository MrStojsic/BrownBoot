using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] SelectorGroup selectorGroup = default;
    [SerializeField] GameObject lastSelectedItemButton = default;

    [SerializeField] private Text title = default;
    //[SerializeField] private SlotScript slotScript;
    // HACK - using Itme until i make a new SlotScript.
    [SerializeField] private Item item = default;
    [SerializeField] private Text actionButtonTitle = default;

    [SerializeField] private Text description = default;

    private void Start()
    {
       
    }
    public void SetDisplay(Item item)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }


        if (lastSelectedItemButton != null)
        {
            lastSelectedItemButton.SetActive(true);
        }

        this.item = item;

        title.text = item.GetTitle();

        if (item.GetType() == typeof(Armour))
        {
            actionButtonTitle.text = "Equip";
        }
        else
        {
            actionButtonTitle.text = "Use";
        }

        description.text = item.GetDescription();
        lastSelectedItemButton = selectorGroup.transform.GetChild(selectorGroup.selectedIndex).gameObject;
        lastSelectedItemButton.SetActive(false);

        transform.SetSiblingIndex(selectorGroup.selectedIndex);

    }
}
