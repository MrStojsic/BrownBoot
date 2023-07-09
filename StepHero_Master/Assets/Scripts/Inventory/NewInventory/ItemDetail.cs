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

public class ItemDetail : InventorySlot
{
    // DATA.
    [SerializeField] private InventorySlot _displayedInventorySlot;

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

                itemQuantityInteractor.ToggleDisplay(false);

                //UpdateStackSizeUI();
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
                inventoryPageManager.ClearEmptyInventorySlot(_displayedInventorySlot);
                InventoryItem = null;
            }
        }
        else
        {
            _stackSizeText.color = Color.clear;
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

    [SerializeField] private ItemQuantityInteractor itemQuantityInteractor = null;
    [SerializeField] private InventoryPageManager inventoryPageManager = null;


    public delegate void ButtonFunction();
    public ButtonFunction leftButtonFunction;
    public ButtonFunction rightButtonFunction;


    [SerializeField] private Text leftButtonText = null;
    [SerializeField] private Text rightButtonText = null;


    public void DisplayItem(InventorySlot inventorySlot)
    {
        if (transform.parent != inventorySlot.transform.parent)
        {
            transform.SetParent(inventorySlot.transform.parent);
        }
        transform.SetSiblingIndex(inventorySlot.Index);

        _displayedInventorySlot = inventorySlot;
        InventoryItem = inventorySlot.InventoryItem;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        Canvas.ForceUpdateCanvases();
    }

    public void SetInteractionInterface(InteractionType interactionType)
    {
        this._interactionType = interactionType;

        switch ((int)_interactionType)
        {
            case 0: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                leftButtonText.transform.parent.gameObject.SetActive(false);
                rightButtonText.text = "Buy";
                rightButtonFunction = SetItemQuantityInteractor;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.
                leftButtonText.transform.parent.gameObject.SetActive(false);
                rightButtonText.text = "Sell";
                rightButtonFunction = SetItemQuantityInteractor;
                break;
            case 2: // PLAYER_USE / DROP, check number of item.
                leftButtonText.text = "Drop";
                leftButtonFunction = SetItemQuantityInteractor;
                leftButtonText.transform.parent.gameObject.SetActive(true);
                rightButtonText.text = "Use";
                rightButtonFunction = InteractWithItem;
                break;
            case 3: // LOOT, check player inventory.
                leftButtonText.text = "Take";
                leftButtonFunction = SetItemQuantityInteractor;
                leftButtonText.transform.parent.gameObject.SetActive(true);
                rightButtonText.text = "Take Max";
                rightButtonFunction = TakeAll;
                break;
            default:
                break;
        }
    }

    public void HideEntireDisplay()
    {
        if (_displayedInventorySlot != null)
        {
            //InventoryItem.InventorySlot = _displayedInventorySlot;
            _displayedInventorySlot.SelectorButton.Deselect();
        }
        gameObject.SetActive(false);
        _displayedInventorySlot = null;
    }

    public void ToggleLongOrShortDescription()
    {
        SetLongOrShortDescription(!descriptionIsShort);
    }

    private void SetLongOrShortDescription(bool isShort)
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
        // Canvas.ForceUpdateCanvases();
        _descriptionAreaLayoutElement.minHeight = (_descriptionText.preferredHeight + _descriptionText.fontSize);
        //_rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (_descriptionText.preferredHeight + _descriptionText.fontSize));
    }

    public void ToggleDescriptionVisibility(bool toggleEnable)
    {
        if (toggleEnable && _descriptionText.gameObject.activeSelf != toggleEnable)
        {
            _descriptionText.gameObject.SetActive(toggleEnable);
        }
        if (toggleEnable == false)
        {
            SetLongOrShortDescription(true);
        }
    }
    public void LeftFunctionInvoke()
    {
        leftButtonFunction.Invoke();
    }
    public void RightFunctionInvoke()
    {
        rightButtonFunction.Invoke();
    }

    private void SetItemQuantityInteractor()
    {
        if (itemQuantityInteractor.gameObject.activeSelf == false)
        {
            itemQuantityInteractor.SetUp();
        }
        else {
            itemQuantityInteractor.SetUp(false);
        }
    }

    private void InteractWithItem()
    {
        PlayerInventory.Instance.UseItem(_inventoryItem);
    }
    private void TakeAll()
    {
        itemQuantityInteractor.SetUp(false);
        itemQuantityInteractor.TakeAllSelectedItem();
    }
}