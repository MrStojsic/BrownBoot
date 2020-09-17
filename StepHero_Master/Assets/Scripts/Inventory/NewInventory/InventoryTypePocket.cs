using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryTypePocket : MonoBehaviour
{
    [SerializeField]
    private SelectorGroup _selectorGroup = default;
    [SerializeField]
    private InventorySlot _inventoryItem = default;

    // DEBUG ONLY. THIS WONT BE NEEDED LATER AS THE ITEMS WILL BE PASSED IN ONE BY ONE.
    [SerializeField]
    private List<InventoryItem> _inventoryItems = default;


    [SerializeField]
    private ItemDetail _itemDetail;
    private List<InventorySlot> _menuItems = new List<InventorySlot>();

    void Awake()
    {
        InitialiseMenuItems();
        //SortInventoryItems();
        SelectFirstItem();
    }

    void InitialiseMenuItems()
    {
        for (int index = 0; index < _inventoryItems.Count; index++)
        {
            AddMenuItem(_inventoryItems[index]);
        }
    }

    void AddMenuItem(InventoryItem inventoryItem)
    {
        InventorySlot newMenuItem;
        newMenuItem = Instantiate(_inventoryItem, _selectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _selectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(inventoryItem);

        _menuItems.Add(newMenuItem);
    }

    private void CallPreviewItem(InventorySlot item)
    {
        _itemDetail.transform.SetSiblingIndex(_selectorGroup.selectedIndex);
        // TODO: We nee to find a way to toggle off the selected InventoryItem Ui and re-enable the last selectd one,
        // so the ItemInventory is hidden while that item is being previewed.
        _itemDetail.PreviewItem(item);
    }

    void SelectFirstItem()
    {
        if (_menuItems != null)
        {
            _itemDetail.Setup(_selectorGroup.selectorButtonsParent);
            _itemDetail.PreviewItem(_menuItems[0]);
        }
    }
    /*
    void SetMenuItems()
    {
        for (int index = 0; index < _inventoryItems.Count; index++)
        {
            SetMenuItem(index);
        }
    }*/


/*
    void SetMenuItem(int index)
    {
        Item item = _inventoryItems[index];
        InventoryItem menuItem = _menuItems[index];

        menuItem.name = item.Title;
        menuItem.transform.Find("Name").GetComponent<Text>().text = item.Title;
        menuItem.transform.Find("Value").GetComponent<Text>().text = item.Price.ToString();

    }*/
    /*
    public void SortInventoryItems(string property = "value")
    {
        switch (property)
        {
            case "value":
                _inventoryItems = _inventoryItems.OrderBy(item => item.value).ToList();
                break;
            default:
                _inventoryItems = _inventoryItems.OrderBy(item => item.label).ToList();
                break;
        }

        SetMenuItems();
    }
    */
}
