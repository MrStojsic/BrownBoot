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
    private ItemDetail _itemDetail;

    [SerializeField]
    private InventorySlot _inventorySlotPrefab = default;

    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    [SerializeField]
    private Transform _pooledInventoryHolder;

    private InventoryTypePocket[] _focusedInventoryPockets;
    public InventoryTypePocket[] FocusedInventoryTypePockets
    {
        get { return _focusedInventoryPockets; }
    }

    [SerializeField]
    private int selectedSlotIndex = 0;
    private int selectedPocketIndex = 0;

    public enum InventoryType
    {
        PLAYER_USE,
        PLAYER_SELL,
        SHOP_BUY,
        LOOT,
    };

    private InventoryType inventoryType;

    public InventoryItem appleToAdd;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            _focusedInventoryPockets[0].AttemptTransferItems(appleToAdd, 1);
        }
    }

    public void InitialiseInventorySlots(InventoryTypePocket[] focusedInventoryTypePockets, InventoryType inventoryType)
    {
        this.inventoryType = inventoryType;
        this._focusedInventoryPockets = focusedInventoryTypePockets;

        bool hasSelectedFirtValidButton = false;

        for (int i = 0; i < focusedInventoryTypePockets.Length; i++)
        {
            if (focusedInventoryTypePockets[i].Count < 1)
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
                if (hasSelectedFirtValidButton == false)
                {
                    hasSelectedFirtValidButton = true;
                    _pocketSelectorGroup.SelectSelectorViaIndex(i);
                }
            }
        }
        // HACK Below was changed ofr testing only, set back when done!
        //_itemDetail.SetInteractionType(inventoryType);
        _itemDetail.SetInteractionType(InventoryType.PLAYER_USE);
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

    public void DeleteItemFromInventory(InventorySlot inventorySlot)
    {
        _focusedInventoryPockets[_pocketSelectorGroup.selectedIndex].SafeForceRemoveItem(inventorySlot.InventoryItem);
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