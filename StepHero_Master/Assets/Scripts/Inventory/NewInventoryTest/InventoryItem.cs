﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour
{
    // DATA.
    private Item _item;
    public Item Item
    {
        get { return _item; }
        private set
        {
            _item = value;
            _title.text = value.Title;
            _icon.sprite = value.Icon;
        }
    }

    private int _numberInInventory;
    public int NumberInInventory
    {
        get { return _numberInInventory; }
        set
        {
            _numberInInventory = value;

            if (_item.StackSize > 1)
            {
                if (_numberInInventory > 1)
                {
                    _stackSizeText.color = Color.white;
                    _stackSizeText.text = _numberInInventory.ToString();
                }
                else if (_numberInInventory == 1)
                {
                    _stackSizeText.color = Color.clear;
                }
                if (_numberInInventory == 0)
                {
                    _item = null;
                    _stackSizeText.color = Color.clear;
                }
            }
            else
            {
                _stackSizeText.color = Color.clear;
            }


        }
    }

    public bool IsEmpty
    {
        get { return _item == null; }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || _numberInInventory < Item.StackSize)
            {
                return false;
            }
            return true;
        }
    }

    // UI.
    [SerializeField] private Text _title = default;
    public Text Title
    {
        get { return _title; }
    }

    [SerializeField] private Image _icon = default;
    public Image Icon
    {
        get { return _icon; }
    }

    [SerializeField] private Text _stackSizeText = default;
    public Text StackSizeText
    {
        get { return _stackSizeText; }
    }

    [SerializeField] private SelectorButton _selectorButton = default;
    public SelectorButton SelectorButton
    {
        get { return _selectorButton; }
    }

    public void Initialise(Item item, int amount)
    {
        Item = item;
        NumberInInventory += amount;
    }

    public bool SetItem(Item item)
    {
        print(item);
        if (IsEmpty)
        {
            Item = item;
            return true;
        }
        // Set Item failed.
        return false;
    }
}
