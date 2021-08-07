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


    private InventoryInteractionManager.InventoryType _inventoryType = 0;
    public InventoryInteractionManager.InventoryType InventoryType
    {
        get { return _inventoryType; }
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


    //[SerializeField] private UnityEvent[] actions;
    string[] lables = { "Use", "Sell", "Buy", "Take All", "Take", "Drop", "Equip", "Unequip" };
    [SerializeField] private Text buttonText1 = null;
    [SerializeField] private Text buttonText2 = null;

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

    public void SetInteractionType(InventoryInteractionManager.InventoryType inventoryType)
    {
        this._inventoryType = inventoryType;

        buttonText1.text = inventoryType == InventoryInteractionManager.InventoryType.LOOT_TAKE ? lables[4] : lables[5];
        buttonText2.text = lables[(int)_inventoryType];
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
        if (_descriptionText.gameObject.activeSelf != toggleEnable)
        {
            _descriptionText.gameObject.SetActive(toggleEnable);
        }
        if (toggleEnable == false)
        {
            SetLongOrShortDescription(true);
        }
    }

    public void SetIQI()
    {
        if (IQI.gameObject.activeSelf == false)
        {
            IQI.SetUp();

        }
    }

    public void RemoveItems(int numberToRemove)
    {
        _inventoryItem.RemoveItems(numberToRemove);
    }

    public void Interact()
    {
        if (_inventoryType == InventoryInteractionManager.InventoryType.LOOT_TAKE)
        {
            IQI.SetUp(false);
            IQI.TakeAllSelectedItem();
            return;
        }
        _inventoryItem.Interact();
    }
}