﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItem
{
    public Item item;

    [SerializeField]
    private int _numberOfItem;
    public int NumberOfItem
    {
        get { return _numberOfItem; }
        set
        {
            if (_numberOfItem <= item.StackSize && _numberOfItem != value)
            {
                _numberOfItem = value;
                if (inventorySlot != null)
                {
                    inventorySlot.UpdateStackSizeUI();
                }
                return;
            }
            Debug.LogWarning("Possible Mistake - " + value + " is more than item.StackSize of " + item.StackSize + " or equal to current " + _numberOfItem);

        }
    }
    public InventorySlot inventorySlot;

    public InventoryItem(Item item, int numberOfItem, InventorySlot inventorySlot)
    {
        this.item = item;
        this.NumberOfItem = numberOfItem;
        this.inventorySlot = inventorySlot;
    }

    // CURRENTLY NOT USED.
    /*
    public bool IsEmpty
    {
        get { return item == null || _numberOfItem <= 0; }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || _numberOfItem < item.StackSize)
            {
                return false;
            }
            return true;
        }
    }*/

    public bool AddFromInventoryItem(InventoryItem sourceInventoryItem, int amountToTransfer)
    {
        if (sourceInventoryItem.item == item && amountToTransfer + _numberOfItem <= item.StackSize && amountToTransfer <= sourceInventoryItem.NumberOfItem)
        {
            NumberOfItem += amountToTransfer;

            sourceInventoryItem.RemoveItems(amountToTransfer);
            return true;

        }
        return false;
    }

    public void RemoveItems(int numberToRemove)
    {
        if (numberToRemove <= NumberOfItem)
        {
            NumberOfItem -= numberToRemove;
        }
    }
}

public class InventorySlot : MonoBehaviour
{
    // DATA.
    protected InventoryItem _inventoryItem;
    public virtual InventoryItem InventoryItem
    {
        get { return _inventoryItem; }
        protected set
        {
            if (value != null)
            {
                _inventoryItem = value;
                _inventoryItem.inventorySlot = this;
                _title.text = value.item.Title;
                _icon.sprite = value.item.Icon;

                UpdateStackSizeUI();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public virtual void UpdateStackSizeUI()
    {
        if (_inventoryItem != null && _inventoryItem.item.StackSize > 1)
        {
            if (_inventoryItem.NumberOfItem > 1)
            {
                _stackSizeText.color = Color.white;
                _stackSizeText.text = _inventoryItem.NumberOfItem.ToString();
            }
            else if (_inventoryItem.NumberOfItem == 1)
            {
                _stackSizeText.color = Color.clear;
            }
            if (_inventoryItem.NumberOfItem == 0)
            {
                InventoryItem = null;
                _stackSizeText.color = Color.clear;
            }
        }
        else
        {
            _stackSizeText.color = Color.clear;
        }
    }

    // UI.
    [SerializeField] protected Text _title = default;
    public Text Title
    {
        get { return _title; }
    }

    [SerializeField] protected Image _icon = default;
    public Image Icon
    {
        get { return _icon; }
    }

    [SerializeField] protected Text _stackSizeText = default;
    public Text StackSizeText
    {
        get { return _stackSizeText; }
    }

    [SerializeField] private SelectorButton _selectorButton = default;
    public SelectorButton SelectorButton
    {
        get { return _selectorButton; }
    }

    public void Initialise(InventoryItem inventorySlot)
    {
        InventoryItem = inventorySlot;
    }

}
