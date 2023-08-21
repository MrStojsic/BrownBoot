using UnityEngine;
using UnityEngine.UI;

public enum InteractionType
{
    SHOP_BUY,
    PLAYER_SELL,
    PLAYER_USE,
    LOOT_TAKE,
};

public class DisplayItemDetail : DisplaySlotDetail
{
    // INVENTORY.
    private InventoryItem _inventoryItem;
    public InventoryItem InventoryItem
    {
        get { return _inventoryItem; }
        protected set
        {
            if (value != null)
            {
                _inventoryItem = value;
                _title.text = value.Item.Title;
                _icon.sprite = value.Item.Icon;
                SetLongOrShortDescription(true);

                _itemQuantityInteractor.ToggleDisplay(false);
                UpdateStackSizeUI();
            }
            else
            {
                HideEntireDisplay();
            }
        }
    }

    // UI.
    [SerializeField] protected Text _stackSizeText = default;
    public Text StackSizeText
    {
        get { return _stackSizeText; }
    }
    [SerializeField] private Text _descriptionText = default;
    public Text DescriptionText
    {
        get { return _descriptionText; }
    }

    private bool descriptionIsShort = true;

    [SerializeField] private LayoutElement _descriptionAreaLayoutElement = null;

    // INTERACTIONS.
    [SerializeField] private InventoryUiManager _inventoryPageManager = null;
    [SerializeField] private ItemQuantityInteractor _itemQuantityInteractor = null;

    private InteractionType _interactionType = 0;
    public InteractionType InteractionType
    {
        get { return _interactionType; }
    }

    public delegate void ButtonFunction();
    public ButtonFunction leftButtonFunction;
    public ButtonFunction rightButtonFunction;

    [SerializeField] private Text _leftButtonText = null;
    [SerializeField] private Text _rightButtonText = null;


    public void UpdateStackSizeUI()
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
                _inventoryPageManager.ClearEmptySlot(_displayedSlot);
                InventoryItem = null;
            }
        }
    }

    public override void DisplayDetail(Slot slot)
    {
        InventoryItem = (slot as InventorySlot).InventoryItem;
        base.DisplayDetail(slot);
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