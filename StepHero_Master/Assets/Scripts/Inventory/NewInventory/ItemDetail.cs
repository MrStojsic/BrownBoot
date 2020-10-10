using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemDetail : InventorySlot
{
    // DATA.
    [SerializeField]private InventorySlot _displayedInventorySlot;

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
                SetLongOrShortDescription(true);

                IQI.ToggleDisplay(false);

                UpdateStackSizeUI();
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
                IIM.DeleteItemFromInventory(_displayedInventorySlot);
                InventoryItem = null;
            }
        }
        else
        {
            _stackSizeText.color = Color.clear;
        }
        _displayedInventorySlot.UpdateStackSizeUI();
    }


    private int inventoryTypeAsInt = -1;

    private bool descriptionIsShort = true;

    // UI.
    [SerializeField] private Text _descriptionText = default;
    public Text DescriptionText
    {
        get { return _descriptionText; }
    }

    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private ItemQuantityInteractor IQI = null;
    [SerializeField] private InventoryInteractionManager IIM = null;


    //[SerializeField] private UnityEvent[] actions;
    string[] lables = { "Use", "Sell", "Buy", "Take All", "Take", "Drop", "Equip", "Unequip" };
    [SerializeField] private Text buttonText1;
    [SerializeField] private Text buttonText2;


    [SerializeField] private GameObject descriptionArea = null;


    public void DisplayItem(InventorySlot inventorySlot)
    {

        if (inventorySlot.transform.parent != transform.parent)
        {
            transform.SetParent(inventorySlot.transform.parent);
            
        }
        transform.SetSiblingIndex(inventorySlot.transform.GetSiblingIndex());
        _displayedInventorySlot = inventorySlot;
        this.InventoryItem = inventorySlot.InventoryItem;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        Canvas.ForceUpdateCanvases();
    }

    public void SetInteractionType(InventoryInteractionManager.InventoryType inventoryType)
    {
        print(inventoryType);
            this.inventoryTypeAsInt = (int)inventoryType;

            buttonText1.text = inventoryType == InventoryInteractionManager.InventoryType.LOOT ? lables[4] : lables[5];

            buttonText2.text = lables[inventoryTypeAsInt];
    }

    public void HideEntireDisplay()
    {
        if (_displayedInventorySlot != null)
        {
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

        Canvas.ForceUpdateCanvases();
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (_descriptionText.preferredHeight + _descriptionText.fontSize));
    }

    public void ToggleDescriptionVisibility(bool toggleEnable)
    {
        if (descriptionArea.activeSelf != toggleEnable)
        {
            descriptionArea.SetActive(toggleEnable);
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
            switch (inventoryTypeAsInt)
            {
                case 0: // DROP. (looking at player inverntory)
                    IQI.SetUp(InventoryItem.NumberOfItem);
                    break;
                case 1: // SELL. (looking at player inverntory)
                    IQI.SetUp(InventoryItem.NumberOfItem);
                    break;
                case 2: // BUY. (looking at shop inverntory)
                    break;
                case 3: // TAKE (looking at Loot inverntory)
                    break;

                default:
                    break;
            }
            IQI.ToggleDisplay(true);
        }


    }

    public void RemoveItems(int numberToRemove)
    {
        _inventoryItem.RemoveItems(numberToRemove);
        UpdateStackSizeUI();
    }

    public void Interact()
    {
        _inventoryItem.Interact();
        UpdateStackSizeUI();
        
    }


}