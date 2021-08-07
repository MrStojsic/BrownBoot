using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractionManager : MonoBehaviour
{ 
    [SerializeField]
    private SelectorGroup _itemSlotSelectorGroup = default;

    [SerializeField]
    private SelectorGroup _pocketSelectorGroup = default;

    [SerializeField]
    private ItemDetail _itemDetail = null;

    [SerializeField]
    private InventorySlot _inventorySlotPrefab = default;

    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    [SerializeField]
    private Transform _pooledInventoryHolder = default;

    // _focusedInventoryPockets is the inventory we are currently displaying, this can be the player, shop or loot depending what we need to be displayed in the UI.
    private InventoryTypePocket[] _focusedInventoryPockets = default;
    /// <summary>
    ///     MUST NEVER BE SET TO THE PLAYERS INVENTORY!
    ///     Non-Players inventory is where we store a reference to the other inventory we need to access,
    ///     this can be the inventory of a shop or loot.
    /// </summary>
    private InventoryTypePocket[] _nonPlayerInventoryTypePockets = default;

    [SerializeField]
    private int selectedSlotIndex = 0;
    private int selectedPocketIndex = 0;

    public enum InventoryType
    {
        PLAYER_USE,
        PLAYER_SELL,
        SHOP_BUY,
        LOOT_TAKE,
    };

    [SerializeField]
    private InventoryType inventoryType;

    // DEBUG
    public InventoryItem testItemToAdd;

    // TESTING
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            // This is a very dirty way to add items to an inventory, not good practice.
            _focusedInventoryPockets[0].AttemptTransferItems(testItemToAdd, 1);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeFocustedInventoryTypePockets(false);
        }
    }
    // TOHERE

    public void SetNonPlayerInventoryTypePockets(InventoryTypePocket[] nonPlayerInventoryTypePockets, InventoryType inventoryType)
    {
        print("called");
        _nonPlayerInventoryTypePockets = nonPlayerInventoryTypePockets;
        this.inventoryType = inventoryType;
    }

    /// <summary>
    /// This takes changes the _focusedInventoryPockets to either the  Players inventory or the current _nonPlayerInventoryTypePockets depending on the bool passed in,
    /// TRUE for the player, FALSE for the Non-Player.
    /// </summary>
    /// <param name="isPlayerInventory"></param>
    public void ChangeFocustedInventoryTypePockets(bool isPlayerInventory)
    {
        if (isPlayerInventory)
        {
            _focusedInventoryPockets = Player_InventoryManager.Instance.inventoryTypePockets;
        }
        else
        {
            _focusedInventoryPockets = _nonPlayerInventoryTypePockets;
        }
        _itemDetail.SetInteractionType(inventoryType);
        InitialiseInventorySlots();
    }


    public void InitialiseInventorySlots()
    {
        bool hasSelectedFirstValidButton = false;

        for (int i = 0; i < _focusedInventoryPockets.Length; i++)
        {
            if (_focusedInventoryPockets[i].Count < 1)
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
                if (hasSelectedFirstValidButton == false)
                {
                    hasSelectedFirstValidButton = true;
                    _pocketSelectorGroup.SelectSelectorViaIndex(i);
                }
            }
        }
        _itemDetail.SetInteractionType(inventoryType);
    }

    public void InitialiseInventorySlotsPageIndex()
    {
        _itemDetail.HideEntireDisplay();
        selectedPocketIndex = _pocketSelectorGroup.selectedIndex;

            int slotIndex = 0;

            for (; slotIndex < _focusedInventoryPockets[selectedPocketIndex].Count; slotIndex++)
            {
                if (_inventorySlots.Count > slotIndex)
                {
                    _inventorySlots[slotIndex].Initialise(_focusedInventoryPockets[selectedPocketIndex].storedItems[slotIndex],slotIndex);
                    _inventorySlots[slotIndex].transform.SetParent(_itemSlotSelectorGroup.selectorButtonsParent);
                    _inventorySlots[slotIndex].gameObject.SetActive(true);
                }
                else
                {
                    AddMenuItem(slotIndex);
                }
            }

            if (_focusedInventoryPockets[selectedPocketIndex].Count < _inventorySlots.Count)
            {
                for (; slotIndex < _inventorySlots.Count; slotIndex++)
                {
                    _inventorySlots[slotIndex].transform.SetParent(_pooledInventoryHolder);

                    // TODO, thers an issues when we change types, the selector group calls the last buttons deslect reenabling the buttons we disabled  here. 
                }
            }
        selectedSlotIndex = 0;
    }

    void AddMenuItem(int slotIndex)
    {
        InventorySlot newMenuItem;
        newMenuItem = Instantiate(_inventorySlotPrefab, _itemSlotSelectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _itemSlotSelectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(_focusedInventoryPockets[selectedPocketIndex].storedItems[slotIndex], slotIndex);
        newMenuItem.transform.name += _inventorySlots.Count.ToString();
        _inventorySlots.Add(newMenuItem);
    }

    private void CallPreviewItem(InventorySlot inventorySlot)
    {
        selectedSlotIndex = inventorySlot.Index;
        _itemDetail.transform.SetSiblingIndex(selectedSlotIndex);
  
        _itemDetail.DisplayItem(inventorySlot);
    }

    void SelectFirstItem()
    {
        if (_inventorySlots != null)
        {
            _itemSlotSelectorGroup.OnButtonSelected(_inventorySlots[selectedSlotIndex].SelectorButton);
        }
    }

    public void RemoveItemFromInventory(InventorySlot inventorySlot)
    {
        _focusedInventoryPockets[_pocketSelectorGroup.selectedIndex].SafeForceRemoveItem(inventorySlot.InventoryItem);

        // Check if the pocket is now empty after removing item from it, if so disable the pocket button icon in the catagory list.
        if (_focusedInventoryPockets[_pocketSelectorGroup.selectedIndex].Count == 0)
        {
            _pocketSelectorGroup.transform.GetChild(_pocketSelectorGroup.selectedIndex).gameObject.SetActive(false);
        }

        PoolInventorySlot(inventorySlot);
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