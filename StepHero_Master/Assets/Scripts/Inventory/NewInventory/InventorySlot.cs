using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int numberOfItem;

    public bool IsEmpty
    {
        get { return item == null || numberOfItem <= 0; }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || numberOfItem < item.StackSize)
            {
                return false;
            }
            return true;
        }
    }
}

public class InventorySlot : MonoBehaviour
{
    // DATA.
    private InventoryItem _inventoryItem;
    public InventoryItem InventoryItem
    {
        get { return _inventoryItem; }
        private set
        {
            if (value != null)
            {
                _inventoryItem = value;
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
        get { return _inventoryItem.numberOfItem; }
        set
        {
            _inventoryItem.numberOfItem = value;

            if (_inventoryItem.item.StackSize > 1)
            {
                if (_inventoryItem.numberOfItem > 1)
                {
                    _stackSizeText.color = Color.white;
                    _stackSizeText.text = _inventoryItem.numberOfItem.ToString();
                }
                else if (_inventoryItem.numberOfItem == 1)
                {
                    _stackSizeText.color = Color.clear;
                }
                if (_inventoryItem.numberOfItem == 0)
                {
                    _inventoryItem = null;
                    _stackSizeText.color = Color.clear;
                }
            }
            else
            {
                _stackSizeText.color = Color.clear;
            }
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

    public void Initialise(InventoryItem inventorySlot)
    {
        InventoryItem = inventorySlot;
    }
}
