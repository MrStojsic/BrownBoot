using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] SelectorGroup selectorGroup;
    [SerializeField] GameObject lastSelectedItemButton;

    [SerializeField] private Text title;
    //[SerializeField] private SlotScript slotScript;
    // HACK - using Itme until i make a new SlotScript.
    [SerializeField] private Item item;
    [SerializeField] private Text actionButtonTitle;

    [SerializeField] private Text description;

    [SerializeField] private Text worth;
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

        Debug.Log(item.Title);

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
        Debug.Log(selectorGroup.selectedIndex);
       lastSelectedItemButton = selectorGroup.transform.GetChild(selectorGroup.selectedIndex).gameObject;
       lastSelectedItemButton.SetActive(false);

       transform.SetSiblingIndex(selectorGroup.selectedIndex);

    }
}
