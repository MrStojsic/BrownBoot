using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemDetail : MonoBehaviour
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
                _title.text = value.InventoryItem.item.Title;
                _icon.sprite = value.InventoryItem.item.Icon;
                _descriptionText.text = value.InventoryItem.item.GetDescription();
                NumberInInventory = value.InventoryItem.NumberOfItem;
                IQI.ToggleDisplay(false);
                SetDescriptionRect();
            }
        }
    }

    private int _numberInInventory;
    public int NumberInInventory
    {
        get { return _numberInInventory; }
        set
        {
            _numberInInventory = value;

            if (_inventorySlot.InventoryItem.item.StackSize > 1)
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

    private int inventoryTypeAsInt = -1;

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

    [SerializeField] private Text _descriptionText = default;
    public Text DescriptionText
    {
        get { return _stackSizeText; }
    }

    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private ItemQuantityInteractor IQI = null;

    //[SerializeField] private UnityEvent[] actions;
    string[] lables = { "Use", "Sell", "Buy", "Take", "Take All", "Drop", "Equip", "Unequip" };
    [SerializeField] private Text buttonText1;
    [SerializeField] private Text buttonText2;

    private void SetDescriptionRect()
    {
        Canvas.ForceUpdateCanvases();
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (_descriptionText.preferredHeight + _descriptionText.fontSize));
    }

    public void Setup(Transform parent)
    {
        transform.SetParent(parent);
        if (_inventorySlot != null)
        {
            _inventorySlot.SelectorButton.Deselect();
        }
    }
    public void PreviewItem(InventorySlot inventoryItem)
    {
        if (inventoryItem.transform.parent != transform.parent)
        {
            Setup(inventoryItem.transform.parent);
            transform.SetSiblingIndex(inventoryItem.transform.GetSiblingIndex());
        }
        InventorySlot = inventoryItem;
    }

    public void PrintItemName()
    {
        print(_inventorySlot.InventoryItem.item.Title);
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
                    IQI.SetUp(_inventorySlot.InventoryItem.NumberOfItem);
                    break;
                case 1: // SELL. (looking at player inverntory)
                    IQI.SetUp(_inventorySlot.InventoryItem.NumberOfItem);
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
}