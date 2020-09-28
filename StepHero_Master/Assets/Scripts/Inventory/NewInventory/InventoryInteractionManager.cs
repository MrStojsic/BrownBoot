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

    private InventoryTypePocket[] focusedInventoryTypePockets;

    void Start()
    {
        //SortInventoryItems();

    }

    public InventoryItem appleToAdd;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Player_InventoryManager.Instance.inventoryTypePockets[0].AttemptTransferItems(appleToAdd, 1);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            print(Player_InventoryManager.Instance.inventoryTypePockets[0].MaxNumberOfItemTransferableFromSource(appleToAdd));
        }
    }

    // TODO: The plan is to pass in a reference to the inventory we want to use, rather than refer to the player.instance etc.
    // we will instead set focusedInventoryTypePockets to the inventory we are focusing on using button in shops etc,
    // and access the pockets we wish to see in the UI by using the buttons index as the pocket index ref of that focused inventory.
    // hope this makes sence later :|

    public void InitialiseInventorySlots(InventoryTypePocket[] focusedInventoryTypePockets)
    {
        this.focusedInventoryTypePockets = focusedInventoryTypePockets;

        selectedTypeButtonIndex = -1;

        bool hasSelectedFirtValidButton = false;

        for (int i = 0; i < focusedInventoryTypePockets.Length; i++)
        {
            if (focusedInventoryTypePockets[i].Count < 1)
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _sideButtonSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
                if (hasSelectedFirtValidButton == false)
                {
                    hasSelectedFirtValidButton = true;
                    _sideButtonSelectorGroup.selectedIndex = i;
                    InitialiseInventorySlotsPageIndex();
                }
            }
        }

    }



    public void InitialiseInventorySlotsPageIndex()
    {
        int pocketIndex = _sideButtonSelectorGroup.selectedIndex;
        if (selectedTypeButtonIndex != pocketIndex)
        {
            selectedTypeButtonIndex = pocketIndex;
            int slotIndex = 0;

            for (; slotIndex < focusedInventoryTypePockets[pocketIndex].Count; slotIndex++)
            {
                if (_inventorySlots.Count > slotIndex)
                {
                    _inventorySlots[slotIndex].Initialise(focusedInventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                    _inventorySlots[slotIndex].transform.SetParent(_itemSlotSelectorGroup.selectorButtonsParent);
                    _inventorySlots[slotIndex].gameObject.SetActive(true);
                }
                else
                {
                    AddMenuItem(focusedInventoryTypePockets[pocketIndex].storedItems[slotIndex]);
                }
            }

            if (focusedInventoryTypePockets[pocketIndex].Count < _inventorySlots.Count)
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