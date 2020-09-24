using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractionManager : MonoBehaviour
{
    [SerializeField]
    private SelectorGroup _itemSlotSelectorGroup = default;

    [SerializeField]
    private SelectorGroup _sideButtonSelectorGroup = default;

    [SerializeField]
    private ItemDetail _itemDetail;

    [SerializeField]
    private InventorySlot _inventorySlotPrefab = default;

    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    [SerializeField]
    private Transform _pooledInventoryHolder;

    private int selectedTypeButtonIndex = -1;

    void Start()
    {
        Player_InitialiseInventorySlots();
        // Player_InitialiseInventorySlotsPageIndex(0);
        //SortInventoryItems();

    }

    public InventoryItem appleToAdd;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Player_InventoryManager.Instance.inventoryTypePockets[0].AddItem(appleToAdd, 5);
        }
    }

    public void Player_InitialiseInventorySlots()
    {
        
        selectedTypeButtonIndex = -1;

        for (int i = 0; i < Player_InventoryManager.Instance.inventoryTypePockets.Length; i++)
        {
            if (Player_InventoryManager.Instance.inventoryTypePockets[i].Count < 1)
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }


    public void Player_InitialiseInventorySlotsPageIndex()
    {
        int pocketIndex = _sideButtonSelectorGroup.selectedIndex;
        if (selectedTypeButtonIndex != pocketIndex)
        {
            selectedTypeButtonIndex = pocketIndex;
            int slotIndex = 0;

            for (; slotIndex < Player_InventoryManager.Instance.inventoryTypePockets[pocketIndex].Count; slotIndex++)
            {
                if (_inventorySlots.Count > slotIndex)
                {
                    _inventorySlots[slotIndex].Initialise(Player_InventoryManager.Instance.inventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                    _inventorySlots[slotIndex].transform.SetParent(_itemSlotSelectorGroup.selectorButtonsParent);
                    _inventorySlots[slotIndex].gameObject.SetActive(true);
                }
                else
                {
                    AddMenuItem(Player_InventoryManager.Instance.inventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                }
            }

            if (Player_InventoryManager.Instance.inventoryTypePockets[pocketIndex].Count < _inventorySlots.Count)
            {
                for (; slotIndex < _inventorySlots.Count; slotIndex++)
                {
                    _inventorySlots[slotIndex].transform.SetParent(_pooledInventoryHolder);

                    // TODO, thers an issues when we change types, the selector group calls the last buttons deslect reenabling the buttons we disabled  here. 
                }
            }
            SelectFirstItem();
        }
    }

    void AddMenuItem(InventoryItem inventoryItem)
    {
        InventorySlot newMenuItem;
        newMenuItem = Instantiate(_inventorySlotPrefab, _itemSlotSelectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _itemSlotSelectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(inventoryItem);

        _inventorySlots.Add(newMenuItem);
    }

    private void CallPreviewItem(InventorySlot item)
    {
        _itemDetail.transform.SetSiblingIndex(_itemSlotSelectorGroup.selectedIndex);
        // TODO: We nee to find a way to toggle off the selected InventoryItem Ui and re-enable the last selectd one,
        // so the ItemInventory is hidden while that item is being previewed.
        _itemDetail.PreviewItem(item);
    }

    void SelectFirstItem()
    {
        if (_inventorySlots != null)
        {
            _itemDetail.Setup(_itemSlotSelectorGroup.selectorButtonsParent);

            _itemSlotSelectorGroup.OnButtonSelected(_inventorySlots[0].SelectorButton);
            // _itemDetail.PreviewItem(_inventorySlots[0]);
        }
    }
    /*
    void SetMenuItems()
    {
        for (int index = 0; index < _inventoryItems.Count; index++)
        {
            SetMenuItem(index);
        }
        SelectFirstItem();
    }*/


    /*
        void SetMenuItem(int index)
        {
            Item item = _inventoryItems[index];
            InventoryItem menuItem = _menuItems[index];

            menuItem.name = item.Title;
            menuItem.transform.Find("Name").GetComponent<Text>().text = item.Title;
            menuItem.transform.Find("Value").GetComponent<Text>().text = item.Price.ToString();

        }*/
    /*
    public void SortInventoryItems(string property = "value")
    {
        switch (property)
        {
            case "value":
                _inventoryItems = _inventoryItems.OrderBy(item => item.value).ToList();
                break;
            default:
                _inventoryItems = _inventoryItems.OrderBy(item => item.label).ToList();
                break;
        }

        SetMenuItems();
    }
    */
}