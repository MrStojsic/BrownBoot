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
                _displayedInventorySlot = value.inventorySlot;

                _inventoryItem = value;
               // _inventoryItem.inventorySlot = this;
                _title.text = value.item.Title;
                _icon.sprite = value.item.Icon;
                _descriptionText.text = value.item.GetDescription();
                SetDescriptionRect();

                IQI.ToggleDisplay(false);

                UpdateStackSizeUI();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void UpdateStackSizeUI()
    {
        if (_inventoryItem != null && _inventoryItem.item.StackSize > 1)
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
                _displayedInventorySlot = null;
                _stackSizeText.color = Color.clear;
            }
        }
        else
        {
            _stackSizeText.color = Color.clear;
        }
    }


    private int inventoryTypeAsInt = -1;

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
    string[] lables = { "Use", "Sell", "Buy", "Take", "Take All", "Drop", "Equip", "Unequip" };
    [SerializeField] private Text buttonText1;
    [SerializeField] private Text buttonText2;

    private void SetDescriptionRect()
    {
        Canvas.ForceUpdateCanvases();
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (_descriptionText.preferredHeight + _descriptionText.fontSize));
    }

    public void DisplayItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.transform.parent != transform.parent)
        {
            transform.SetParent(inventorySlot.transform.parent);
            
        }
        transform.SetSiblingIndex(inventorySlot.transform.GetSiblingIndex());
        this.InventoryItem = inventorySlot.InventoryItem;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    public void SetInteractionType(InventoryInteractionManager.InventoryType inventoryType)
    {
        if (this.inventoryTypeAsInt != (int)inventoryType)
        {
            this.inventoryTypeAsInt = (int)inventoryType;

            buttonText1.text = inventoryType == InventoryInteractionManager.InventoryType.LOOT ? lables[4] : lables[5];

            buttonText2.text = lables[inventoryTypeAsInt];
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
        _displayedInventorySlot.InventoryItem.RemoveItems(numberToRemove);
        UpdateStackSizeUI();
    }

}