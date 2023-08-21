using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotUiManager : UiWindow
{
    [SerializeField]
    private SelectorGroup _slotSelectorGroup = default;

    [SerializeField]
    protected SelectorGroup _pocketSelectorGroup = default;

    [SerializeField]
    protected DisplaySlotDetail _displayDetailSlot = null;

    [SerializeField]
    private Slot _slotPrefab = default;

    [SerializeField]
    private List<Slot> _slots = new List<Slot>();

    [SerializeField]
    private Transform _pooledSlotHolder = default;

    protected ICountable[] slotPockets;

    [SerializeField]
    private int selectedSlotIndex = 0;
    protected int selectedPocketIndex = 0;

    public void test()
    {
    }
    public void InitialisePockets()
    {
        bool hasSelectedFirstValidButton = false;

        // If an error is being thrown here press M to set the merchants inventory up for now, or check that the _nonPlayerInventory variable references to something.
        for (int i = 0; i < slotPockets.Length; i++)
        {
            if (slotPockets[i].Count < 1)
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
    }
    
    public void InitialisePocketedSlots()
    {
        _displayDetailSlot.HideEntireDisplay();
        selectedPocketIndex = _pocketSelectorGroup.selectedIndex;

        int slotIndex = 0;

        for (; slotIndex < slotPockets[selectedPocketIndex].Count; slotIndex++)
        {
            if (_slots.Count > slotIndex)
            {
                InitializeNewSlot(_slots[slotIndex], slotIndex);
                _slots[slotIndex].transform.SetParent(_slotSelectorGroup.selectorButtonsParent);
                _slots[slotIndex].transform.SetSiblingIndex(slotIndex);
                _slots[slotIndex].gameObject.SetActive(true);
            }
            else
            {
                AddNewSlot(slotIndex);
            }
        }

        if (slotPockets[selectedPocketIndex].Count < _slots.Count)
        {
            for (; slotIndex < _slots.Count; slotIndex++)
            {
                _slots[slotIndex].transform.SetParent(_pooledSlotHolder);
            }
        }
    }

    void AddNewSlot(int slotIndex)
    {
        Slot newMenuItem;
        newMenuItem = Instantiate(_slotPrefab, _slotSelectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _slotSelectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallDisplayDetail(newMenuItem));
        InitializeNewSlot(newMenuItem, slotIndex);
        newMenuItem.transform.name += _slots.Count.ToString();
        _slots.Add(newMenuItem);
    }

    protected virtual void InitializeNewSlot(Slot newSlot, int slotIndex)
    {
        throw new NotImplementedException("InitializeNewSlot in " + gameObject.name + "  not implimented!"); 
    }

    private void CallDisplayDetail(Slot slot)
    {
        print(slot.Index + " : " + slot.transform.GetSiblingIndex());

        selectedSlotIndex = slot.Index;
        _displayDetailSlot.DisplayDetail(slot);
    }

    void SelectFirstItem()
    {
        if (_slots != null)
        {
            _slotSelectorGroup.OnButtonSelected(_slots[selectedSlotIndex].SelectorButton);
        }
    }

    public virtual void ClearEmptySlot(Slot slot)
    {
        // Check if the pocket is now empty after removing item from it, if so disable the pocket button icon in the catagory list.
        if (slotPockets[_pocketSelectorGroup.selectedIndex].Count == 0)
        {
            _pocketSelectorGroup.transform.GetChild(_pocketSelectorGroup.selectedIndex).gameObject.SetActive(false);
        }
        PoolSlot(slot);
    }

    public void PoolSlot(Slot emptySlot)
    {
        emptySlot.transform.SetParent(_pooledSlotHolder);
    }

    // -The following 3 functions are used for sorting the items by name or value, its a WIP.
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

