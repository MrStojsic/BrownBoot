using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractionManager : MonoBehaviour
{ 
    [SerializeField]
    private SelectorGroup _itemSlotSelectorGroup = default;

    [SerializeField]
    private SelectorGroup _sideButtonSelectorGroup = default;

    [SerializeField]
    private ItemDetail _itemDetail;

    [SerializeField]
    private InventorySlot _inventorySlotPrefab = default;

    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    [SerializeField]
    private Transform _pooledInventoryHolder;

    private InventoryTypePocket[] _focusedInventoryTypePockets;
    public InventoryTypePocket[] FocusedInventoryTypePockets
    {
        get { return _focusedInventoryTypePockets; }
    }

    [SerializeField]
    private int selectedIndex = 0;

    public enum InventoryType
    {
        PLAYER_USE,
        PLAYER_SELL,
        SHOP_BUY,
        LOOT,
    };

    private InventoryType inventoryType;


    void Start()
    {
        //SortInventoryItems();

    }

    public InventoryItem appleToAdd;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            _focusedInventoryTypePockets[0].AttemptTransferItems(appleToAdd, 1);
        }
    }

    public void InitialiseInventorySlots(InventoryTypePocket[] focusedInventoryTypePockets, InventoryType inventoryType)
    {
        this.inventoryType = inventoryType;
        this._focusedInventoryTypePockets = focusedInventoryTypePockets;

        bool hasSelectedFirtValidButton = false;

        for (int i = 0; i < focusedInventoryTypePockets.Length; i++)
        {
            if (focusedInventoryTypePockets[i].Count < 1)
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
                if (hasSelectedFirtValidButton == false)
                {
                    hasSelectedFirtValidButton = true;
                    _sideButtonSelectorGroup.SelectSelectorViaIndex(i);
                }
            }
        }
        _itemDetail.SetInteractionType(inventoryType);
    }



    public void InitialiseInventorySlotsPageIndex()
    {
        int pocketIndex = _sideButtonSelectorGroup.selectedIndex;

            int slotIndex = 0;

            for (; slotIndex < _focusedInventoryTypePockets[pocketIndex].Count; slotIndex++)
            {
                if (_inventorySlots.Count > slotIndex)
                {
                    _inventorySlots[slotIndex].Initialise(_focusedInventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                    _inventorySlots[slotIndex].transform.SetParent(_itemSlotSelectorGroup.selectorButtonsParent);
                    _inventorySlots[slotIndex].gameObject.SetActive(true);
                }
                else
                {
                    AddMenuItem(_focusedInventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                }
            }

            if (_focusedInventoryTypePockets[pocketIndex].Count < _inventorySlots.Count)
            {
                for (; slotIndex < _inventorySlots.Count; slotIndex++)
                {
                    _inventorySlots[slotIndex].transform.SetParent(_pooledInventoryHolder);

                    // TODO, thers an issues when we change types, the selector group calls the last buttons deslect reenabling the buttons we disabled  here. 
                }
            }
        selectedIndex = 0;
        SelectFirstItem();

    }

    void AddMenuItem(InventoryItem inventoryItem)
    {
        InventorySlot newMenuItem;
        newMenuItem = Instantiate(_inventorySlotPrefab, _itemSlotSelectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _itemSlotSelectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(inventoryItem);
        newMenuItem.transform.name += _inventorySlots.Count.ToString();
        _inventorySlots.Add(newMenuItem);
    }

    private void CallPreviewItem(InventorySlot inventorySlot)
    {
        _itemDetail.transform.SetSiblingIndex(_itemSlotSelectorGroup.selectedIndex);
        // TODO: We nee to find a way to toggle off the selected InventoryItem Ui and re-enable the last selectd one,
        // so the ItemInventory is hidden while that item is being previewed.
        _itemDetail.DisplayItem(inventorySlot);
    }

    void SelectFirstItem()
    {
        if (_inventorySlots != null)
        { 
            print(selectedIndex);
            _itemSlotSelectorGroup.OnButtonSelected(_focusedInventoryTypePockets[_sideButtonSelectorGroup.selectedIndex].storedItems[selectedIndex].inventorySlot.SelectorButton);
        }
    }

    public void DeleteItemFromInventory(InventorySlot inventorySlot)
    {
        _focusedInventoryTypePockets[_sideButtonSelectorGroup.selectedIndex].SafeForceRemoveItem(inventorySlot.InventoryItem);
        PoolInventorySlot(inventorySlot);

        if (_focusedInventoryTypePockets[_sideButtonSelectorGroup.selectedIndex].Count < selectedIndex)
        {
            selectedIndex--;
        }

        SelectFirstItem();
    }

    public void PoolInventorySlot(InventorySlot inventorySlot)
    {

        inventorySlot.transform.SetParent(_pooledInventoryHolder);
    }
    /*
    void SetMenuItems()
    {
        for (int index = 0; index < _inventoryItems.Count; index++)
        {
            SetMenuItem(index);
        }
        SelectFirstItem();
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