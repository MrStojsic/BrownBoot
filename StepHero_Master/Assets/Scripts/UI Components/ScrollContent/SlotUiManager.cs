using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//- This interface is used for all slotPockets classes as a general accessor to the pockets.
public interface ICountable
{
    int Count { get; }
}

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
    protected List<Slot> _existingSlots = new List<Slot>();

    [SerializeField]
    private Transform _pooledSlotHolder = default;

    protected ICountable[] slotPockets;

    [SerializeField]
    protected int selectedSlotIndex = 0;
    protected int selectedPocketIndex = 0;

	//- Set pockets to only be displayed if they contain slots with data in them.
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
    
    public void InitialiseSlotsInPockets()
    {
        _displayDetailSlot.HideEntireDisplay();
        selectedPocketIndex = _pocketSelectorGroup.selectedIndex;

        int index = 0;

        for (; index < slotPockets[selectedPocketIndex].Count; index++)
        {
            if (_existingSlots.Count > index)
            {
                SetSpecializedSlotFunctionality(_existingSlots[index], index);
                _existingSlots[index].transform.SetParent(_slotSelectorGroup.selectorButtonsParent);
                _existingSlots[index].transform.SetSiblingIndex(index);
                _existingSlots[index].gameObject.SetActive(true);
            }
            else
            {
                CreateNewSlot(index);
            }
        }

        if (slotPockets[selectedPocketIndex].Count < _existingSlots.Count)
        {
            for (; index < _existingSlots.Count; index++)
            {
                PoolSlot(_existingSlots[index]);
            }
        }
    }

    void CreateNewSlot(int slotIndex)
    {
        Slot newSlot;
        newSlot = Instantiate(_slotPrefab, _slotSelectorGroup.selectorButtonsParent);
        newSlot.SelectorButton.selectorGroup = _slotSelectorGroup;
        newSlot.SelectorButton.AddListenerActionToOnSelected(() => CallDisplayDetail(newSlot));
        SetSpecializedSlotFunctionality(newSlot, slotIndex);
        newSlot.transform.name += _existingSlots.Count.ToString();
        _existingSlots.Add(newSlot);
    }

    protected virtual void SetSpecializedSlotFunctionality(Slot newSlot, int slotIndex)
    {
        throw new NotImplementedException("InitializeNewSlot in " + gameObject.name + "  not implimented!"); 
    }

    private void CallDisplayDetail(Slot slot)
    {
		selectedSlotIndex = slot.Index;
        _displayDetailSlot.DisplayDetail(slot);
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

