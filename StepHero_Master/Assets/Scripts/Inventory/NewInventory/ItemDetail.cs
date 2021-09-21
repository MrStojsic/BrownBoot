using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

                IQI.ToggleDisplay(false);

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
        if (_inventoryItem != null && _inventoryItem.Item.StackSize > 1)
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
                IIM.RemoveItemFromInventory(_displayedInventorySlot);
                InventoryItem = null;
            }
        }
        else
        {
            _stackSizeText.color = Color.clear;
        }
    }


    private InventoryInteractionManager.InteractionType _interactionType = 0;
    public InventoryInteractionManager.InteractionType InteractionType
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

    [SerializeField] private ItemQuantityInteractor IQI = null;
    [SerializeField] private InventoryInteractionManager IIM = null;


    public delegate void ButtonFunction();
    public ButtonFunction leftButtonFunction;
    public ButtonFunction rightButtonFunction;


    [SerializeField] private Text leftButtonText = null;
    [SerializeField] private Text rightButtonText = null;


    public void DisplayItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.transform.parent != transform.parent)
        {
            transform.SetParent(inventorySlot.transform.parent);
        }
        transform.SetSiblingIndex(inventorySlot.transform.GetSiblingIndex());

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

    public void SetInteractionInterface(InventoryInteractionManager.InteractionType interactionType)
    {
        this._interactionType = interactionType;

        switch ((int)_interactionType)
        {

            case 0: // PLAYER_USE / DROP, check number of item.
                leftButtonText.text = "Drop";
                leftButtonFunction = SetIQI;
                leftButtonText.transform.parent.gameObject.SetActive(true);
                rightButtonText.text = "Use";
                rightButtonFunction = InteractWithItem;
                break;
            case 1: // PLAYER_SELL, check number of item and number shop can afford.
                leftButtonText.transform.parent.gameObject.SetActive(false);
                rightButtonText.text = "Sell";
                rightButtonFunction = SetIQI;
                break;
            case 2: // SHOP_BUY, check both player inventory and number of item and number player can afford.
                leftButtonText.transform.parent.gameObject.SetActive(false);
                rightButtonText.text = "Buy";
                rightButtonFunction = SetIQI;
                break;
            case 3: // LOOT, check player inventory.
                leftButtonText.text = "Take";
                leftButtonFunction = SetIQI;
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

    private void SetIQI()
    {
        if (IQI.gameObject.activeSelf == false)
        {
            IQI.SetUp();
        }
        else {
            IQI.SetUp(false);
        }
    }

    private void InteractWithItem()
    {
        _inventoryItem.Interact();
    }
    private void TakeAll()
    {
        IQI.SetUp(false);
        IQI.TakeAllSelectedItem();
    }
}