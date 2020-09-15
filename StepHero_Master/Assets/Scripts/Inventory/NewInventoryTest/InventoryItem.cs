using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int numberOfItem;
}

public class InventoryItem : MonoBehaviour
{
    // DATA.
    private InventorySlot _inventorySlot;
    public InventorySlot InventorySlot
    {
        get { return _inventorySlot; }
        private set
        {
            if (value != null)
            {
                _inventorySlot = value;
                _title.text = value.item.Title;
                _icon.sprite = value.item.Icon;
                NumberInInventory = value.numberOfItem;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public int NumberInInventory
    {
        get { return _inventorySlot.numberOfItem; }
        set
        {
            _inventorySlot.numberOfItem = value;

            if (_inventorySlot.item.StackSize > 1)
            {
                if (_inventorySlot.numberOfItem > 1)
                {
                    _stackSizeText.color = Color.white;
                    _stackSizeText.text = _inventorySlot.numberOfItem.ToString();
                }
                else if (_inventorySlot.numberOfItem == 1)
                {
                    _stackSizeText.color = Color.clear;
                }
                if (_inventorySlot.numberOfItem == 0)
                {
                    _inventorySlot = null;
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
        get { return _inventorySlot == null || _inventorySlot.numberOfItem <= 0; }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || _inventorySlot.numberOfItem < _inventorySlot.item.StackSize)
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

    public void Initialise(InventorySlot inventorySlot)
    {
        InventorySlot = inventorySlot;
    }
}
