using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    // DATA.
    private Item _item;
    public Item Item
    {
        get { return _item; }
        private set
        {
            _item = value;
            _title.text = value.Title;
            _icon.sprite = value.Icon;
            _descriptionText.text = value.GetDescription();
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

            if (_item.StackSize > 1)
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
                    _item = null;
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
    }
    public void PreviewItem(InventoryItem inventoryItem)
    {
        Item = inventoryItem.Item;
        NumberInInventory = inventoryItem.NumberInInventory;
    }
}