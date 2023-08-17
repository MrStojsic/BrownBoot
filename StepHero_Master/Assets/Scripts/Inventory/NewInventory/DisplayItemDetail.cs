using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public enum InteractionType
{
    SHOP_BUY,
    PLAYER_SELL,
    PLAYER_USE,
    LOOT_TAKE,
};

public class DisplayItemDetail : InventorySlot
{
    // DATA.
    [SerializeField]
    private InventorySlot _displayedInventorySlot;

    public override InventoryItem InventoryItem
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

                SetLongOrShortDescription(true);

                _itemQuantityInteractor.ToggleDisplay(false);
            }
            else
            {
                HideEntireDisplay();
            }
        }
    }

    public override void UpdateStackSizeUI()
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
                _inventoryPageManager.ClearEmptyInventorySlot(_displayedInventorySlot);
                InventoryItem = null;
            }
        }
    }

    private InteractionType _interactionType = 0;
    public InteractionType InteractionType
    {
        get { return _interactionType; }
    }

    private bool descriptionIsShort = true;

    // UI.
    [SerializeField] private Text _descriptionText = default;
    public Text DescriptionText
    {
        get { return _descriptionText; }
    }
    [SerializeField] private LayoutElement _descriptionAreaLayoutElement = null;

    [SerializeField] private RectTransform _rectTransform = null;

    [SerializeField] private ItemQuantityInteractor _itemQuantityInteractor = null;
    [SerializeField] private InventoryPageManager _inventoryPageManager = null;


    public delegate void ButtonFunction();
    public ButtonFunction leftButtonFunction;
    public ButtonFunction rightButtonFunction;


    [SerializeField] private Text _leftButtonText = null;
    [SerializeField] private Text _rightButtonText = null;


    public void DisplayItem(InventorySlot inventorySlot)
    {
        if (transform.parent != inventorySlot.transform.parent)
        {
            transform.SetParent(inventorySlot.transform.parent);
        }
        transform.SetSiblingIndex(inventorySlot.Index);

        //- Set inventoryItem inventorySlot back to its original slot so the slot updates if any changes were made.
        if (_displayedInventorySlot != null)
        {
            InventoryItem.InventorySlot = _displayedInventorySlot;
        }

        _displayedInventorySlot = inventorySlot;
        InventoryItem = inventorySlot.InventoryItem;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        Canvas.ForceUpdateCanvases();
    }

    public void HideEntireDisplay()
    {
        if (_displayedInventorySlot != null)
        {
            InventoryItem.InventorySlot = _displayedInventorySlot;
            _displayedInventorySlot.SelectorButton.Deselect();
        }
        gameObject.SetActive(false);
        _displayedInventorySlot = null;
    }

    public void ToggleLongOrShortDescription()
    {
        SetLongOrShortDescription(!descriptionIsShort);
    }

    public void SetLongOrShortDescription(bool isShort)
    {
        descriptionIsShort = isShort;
        if (isShort)
        {
            _descriptionText.text = _inventoryItem.Item.GetShortDescription();
        }
        else
        {
            _descriptionText.text = _inventoryItem.Item.GetLongDescription();
        }
        _descriptionAreaLayoutElement.minHeight = (_descriptionText.preferredHeight + _descriptionText.fontSize);
    }

    private void SetItemQuantityInteractor()
    {
        if (_itemQuantityInteractor.gameObject.activeSelf == false)
        {
            _itemQuantityInteractor.SetUp();
        }
        else
        {
            _itemQuantityInteractor.SetUp(false);
        }
    }

    public void SetInteractionInterface(InteractionType interactionType)
    {
        this._interactionType = interactionType;

        switch ((int)_interactionType)
        {
            case 0: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                _leftButtonText.transform.parent.gameObject.SetActive(false);
                _rightButtonText.text = "Buy";
                rightButtonFunction = SetItemQuantityInteractor;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.
                _leftButtonText.transform.parent.gameObject.SetActive(false);
                _rightButtonText.text = "Sell";
                rightButtonFunction = SetItemQuantityInteractor;
                break;
            case 2: // PLAYER_USE / DROP, check number of item.
                _leftButtonText.text = "Drop";
                leftButtonFunction = SetItemQuantityInteractor;
                _leftButtonText.transform.parent.gameObject.SetActive(true);
                _rightButtonText.text = "Use";
                rightButtonFunction = UseItem;
                break;
            case 3: // LOOT, check player inventory.
                _leftButtonText.text = "Take";
                leftButtonFunction = SetItemQuantityInteractor;
                _leftButtonText.transform.parent.gameObject.SetActive(true);
                _rightButtonText.text = "Take Max";
                rightButtonFunction = TakeAll;
                break;
            default:
                break;
        }
    }

    public void LeftFunctionInvoke()
    {
        leftButtonFunction?.Invoke();
    }
    public void RightFunctionInvoke()
    {
        rightButtonFunction?.Invoke();
    }

    // Interactions.
    private void UseItem()
    {
        PlayerInventory.Instance.UseItem(_inventoryItem);
    }
    private void TakeAll()
    {
        _itemQuantityInteractor.SetUp(false);
        _itemQuantityInteractor.TakeAllItem();
    }
}