﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] protected Item _item;
    public Item Item
    {
        get { return _item; }
    }

    [SerializeField]
    protected int _numberOfItem;
    public int NumberOfItem
    {
        get { return _numberOfItem; }
        protected set
        {
            if (_numberOfItem <= Item.StackSize)
            {
                _numberOfItem = value;

                _inventorySlot?.UpdateStackSizeUI();

                return;
            }
            Debug.LogWarning("Possible Mistake - " + value + " is more than item.StackSize of " + Item.StackSize + " or equal to current " + _numberOfItem);
        }
    }

    [SerializeField] protected InventorySlot _inventorySlot;
    public InventorySlot InventorySlot
    {
        get { return _inventorySlot; }
        set {
                _inventorySlot = value;

            _inventorySlot?.UpdateStackSizeUI();
        }
    }

    public InventoryItem(Item item, int numberOfItem, InventorySlot inventorySlot = null)
    {
        _item = item;
        InventorySlot = inventorySlot;
        NumberOfItem = numberOfItem;
    }

    public virtual bool AddFromInventoryItem(InventoryItem sourceInventoryItem, int amountToTransfer)
    {
        if (sourceInventoryItem.Item == Item && amountToTransfer + _numberOfItem <= Item.StackSize && amountToTransfer <= sourceInventoryItem.NumberOfItem)
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

    public virtual bool Interact()
    {
        if (_item is IUseable && ((IUseable)_item).Use())
        {
            RemoveItems(1);
            return true;
        }
        return false;
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
                _title.text = value.Item.Title;
                _icon.sprite = value.Item.Icon;

                _inventoryItem.InventorySlot = this;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public virtual void UpdateStackSizeUI()
    {
        if (_inventoryItem != null)
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
        // TODO i dont think this even needs to be here, it should already be disabled hence no need to update the colour.        else
        {
            _stackSizeText.color = Color.clear;
        }
    }

    private int _index;
    public int Index { get { return _index; } }

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

    public void Initialise(InventoryItem inventoryItem, int index)
    {
        if (_inventoryItem != null)
        {
            _inventoryItem.InventorySlot = null;
        }
        InventoryItem = inventoryItem;
        _index = index;
    }
}
