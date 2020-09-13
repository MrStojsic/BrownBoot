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
    private InventoryItem _inventoryItem = default;
    [SerializeField]
    private List<Item> _inventoryItems = default;


    [SerializeField]
    private ItemDetail _itemDetail;
    private List<InventoryItem> _menuItems = new List<InventoryItem>();

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

    void AddMenuItem(Item item)
    {
        InventoryItem newMenuItem;
        newMenuItem = Instantiate(_inventoryItem, _selectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _selectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(item, 99);

        _menuItems.Add(newMenuItem);
    }

    private void CallPreviewItem(InventoryItem item)
    {
        _itemDetail.transform.SetSiblingIndex(_selectorGroup.selectedIndex);
        // TODO: We nee to find a way to toggle off the selected InventoryItem Ui and re-enable the last selectd one,
        // so the ItemInventory is hidden while that item is being previewed.
        _itemDetail.PreviewItem(item);
    }

    void SelectFirstItem()
    {
        if (_inventoryItems != null)
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
