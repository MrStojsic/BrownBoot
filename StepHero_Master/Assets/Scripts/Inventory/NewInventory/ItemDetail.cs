using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    // DATA.
    private InventorySlot _inventoryItem;
    public InventorySlot InventoryItem
    {
        get { return _inventoryItem; }
        private set
        {
            _inventoryItem = value;
            _title.text = value.InventoryItem.item.Title;
            _icon.sprite = value.InventoryItem.item.Icon;
            _descriptionText.text = value.InventoryItem.item.GetDescription();
            NumberInInventory = value.NumberInInventory;
            SetDescriptionRect();
        }
    }

    private int _numberInInventory;
    public int NumberInInventory
    {
        get { return _numberInInventory; }
        set
        {
            _numberInInventory = value;

            if (_inventoryItem.InventoryItem.item.StackSize > 1)
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

    [SerializeField] private Text _descriptionText = default;
    public Text DescriptionText
    {
        get { return _stackSizeText; }
    }

    [SerializeField] private RectTransform _rectTransform;

    private void SetDescriptionRect()
    {
        Canvas.ForceUpdateCanvases();
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (_descriptionText.preferredHeight + _descriptionText.fontSize));
    }

    public void Setup(Transform parent)
    {
        transform.SetParent(parent);
        if (_inventoryItem != null)
        {
            _inventoryItem.SelectorButton.Deselect();
        }
    }
    public void PreviewItem(InventorySlot inventoryItem)
    {
        if (inventoryItem.transform.parent != transform.parent)
        {
            Setup(inventoryItem.transform.parent);
            transform.SetSiblingIndex(inventoryItem.transform.GetSiblingIndex());
        }
        InventoryItem = inventoryItem;
    }
}