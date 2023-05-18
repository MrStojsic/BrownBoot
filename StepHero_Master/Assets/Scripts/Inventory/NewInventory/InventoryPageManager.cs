using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPageManager : MonoBehaviour
{
    private static InventoryPageManager _instance;
    public static InventoryPageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryPageManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private SelectorGroup _itemSlotSelectorGroup = default;

    [SerializeField]
    private SelectorGroup _pocketSelectorGroup = default;

    [SerializeField]
    private ItemDetail _itemDetail = null;

    [SerializeField]
    private InventorySlot _inventorySlotPrefab = default;

    [SerializeField]
    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    [SerializeField]
    private Transform _pooledInventoryHolder = default;

    // _focusedInventoryPockets is the inventory we are currently displaying, this can be the player, shop or loot depending what we need to be displayed in the UI.
    private Inventory _focusedInventory = default;

    private Inventory _playerInventory = default;
    public Inventory PlayerInventory
    {
        get { return _playerInventory; }
        set
        {
            if (value is Player_InventoryManager)
            {
                _playerInventory = value;
            }
            else
            {
                Debug.LogError("_playerInventory MUST be of type Player_InventoryManager silly.");
            }
        }
    }
    /// <summary>
    ///     MUST NEVER BE SET TO THE PLAYERS INVENTORY!
    ///     Non-Players inventory is where we store a reference to the other inventory we need to access,
    ///     this can be the inventory of a shop or loot.
    /// </summary>
    [SerializeField]
    private Inventory _nonPlayerInventory = default;
    public Inventory NonPlayerInventory
    {
        get { return _nonPlayerInventory; }
        set
        {
            if (!(value is Player_InventoryManager))
            {
                _nonPlayerInventory = value;
            }
            else
            {
                Debug.LogError("_playerInventory CANNOT be of type Player_InventoryManager silly.");
            }
        }
    }

    [SerializeField]
    private int selectedSlotIndex = 0;
    private int selectedPocketIndex = 0;



    [SerializeField]
    private InteractionType _interactionType;

    // DEBUG
    public InventoryItem testItemToAdd;

    // TESTING
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("Pressed S - Set Inventory to Sell from Player.");
            ChangeFocusedInventory(true);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("Pressed P - Set Inventory to purchase from Merchant.");
            ChangeFocusedInventory(false);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            Debug.Log("Pressed T - Gave player test item.");
            _playerInventory.InventoryTypePockets[(int)testItemToAdd.Item.ItemType].AttemptReceiveItems(testItemToAdd, 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("Pressed D - Deloaded mechant inventory.");
            NonPlayerInventory = null;
            ChangeFocusedInventory(true);
        }

    }
    // TOHERE

    public void SetInventory(Inventory inventory)
    {
        if (inventory is Player_InventoryManager)
        {
            PlayerInventory = inventory;
        }
        else
        {
            _nonPlayerInventory = inventory;
        }
    }

    /// <summary>
    /// This takes changes the _focusedInventoryPockets to either the  Players inventory or the current _nonPlayerInventoryTypePockets depending on the bool passed in,
    /// TRUE for the player, FALSE for the Non-Player.
    /// </summary>
    /// <param name="isPlayerInventory"></param>
    public void ChangeFocusedInventory(bool isPlayerInventory)
    {
        if (isPlayerInventory)
        {
            _focusedInventory = _playerInventory;
            // - The 'is' operator in C# is used to check the object type and it returns a bool value: true if the object is the same type and false if not.
            //   So if _nonPlayerInventory is a merchant, pass PLAYER_SELL, otherwise pass PLAYER_USE as the parameter in SetInteractionInterface.
            _itemDetail.SetInteractionInterface(_nonPlayerInventory is Merchant_InventoryManager ? InteractionType.PLAYER_SELL : InteractionType.PLAYER_USE);
        }
        else
        {
            _focusedInventory = _nonPlayerInventory;
            _itemDetail.SetInteractionInterface(_nonPlayerInventory is Merchant_InventoryManager ? InteractionType.SHOP_BUY : InteractionType.LOOT_TAKE);
        }
        InitialiseInventorySlots();
    }


    public void InitialiseInventorySlots()
    {
        bool hasSelectedFirstValidButton = false;

        // If an error is being thrown here press M to set the merchants inventory up for now, or check that the _nonPlayerInventory variable references to something.
        for (int i = 0; i < _focusedInventory.InventoryTypePockets.Length; i++)
        {
            if (_focusedInventory.InventoryTypePockets[i].Count < 1)
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _pocketSelectorGroup.transform.GetChild(i).gameObject.SetActive(true);
                if (hasSelectedFirstValidButton == false)
                {
                    hasSelectedFirstValidButton = true;
                    _pocketSelectorGroup.SelectSelectorViaIndex(i);
                }
            }
        }
        //_itemDetail.SetInteractionInterface(_interactionType);
    }

    public void InitialiseInventorySlotsPageIndex()
    {
        _itemDetail.HideEntireDisplay();
        selectedPocketIndex = _pocketSelectorGroup.selectedIndex;

            int slotIndex = 0;

            for (; slotIndex < _focusedInventory.InventoryTypePockets[selectedPocketIndex].Count; slotIndex++)
            {
                if (_inventorySlots.Count > slotIndex)
                {
                    _inventorySlots[slotIndex].Initialise(_focusedInventory.InventoryTypePockets[selectedPocketIndex].storedItems[slotIndex],slotIndex);
                    _inventorySlots[slotIndex].transform.SetParent(_itemSlotSelectorGroup.selectorButtonsParent);
                    _inventorySlots[slotIndex].gameObject.SetActive(true);
                }
                else
                {
                    AddMenuItem(slotIndex);
                }
            }

            if (_focusedInventory.InventoryTypePockets[selectedPocketIndex].Count < _inventorySlots.Count)
            {
                for (; slotIndex < _inventorySlots.Count; slotIndex++)
                {
                    _inventorySlots[slotIndex].transform.SetParent(_pooledInventoryHolder);

                    // TODO, thers an issues when we change types, the selector group calls the last buttons deslect reenabling the buttons we disabled  here. 
                }
            }
        selectedSlotIndex = 0;
    }

    void AddMenuItem(int slotIndex)
    {
        InventorySlot newMenuItem;
        newMenuItem = Instantiate(_inventorySlotPrefab, _itemSlotSelectorGroup.selectorButtonsParent);
        newMenuItem.SelectorButton.selectorGroup = _itemSlotSelectorGroup;
        newMenuItem.SelectorButton.AddListenerActionToOnSelected(() => CallPreviewItem(newMenuItem));
        newMenuItem.Initialise(_focusedInventory.InventoryTypePockets[selectedPocketIndex].storedItems[slotIndex], slotIndex);
        newMenuItem.transform.name += _inventorySlots.Count.ToString();
        _inventorySlots.Add(newMenuItem);
    }

    private void CallPreviewItem(InventorySlot inventorySlot)
    {
        selectedSlotIndex = inventorySlot.Index;
        _itemDetail.transform.SetSiblingIndex(selectedSlotIndex);
  
        _itemDetail.DisplayItem(inventorySlot);
    }

    void SelectFirstItem()
    {
        if (_inventorySlots != null)
        {
            _itemSlotSelectorGroup.OnButtonSelected(_inventorySlots[selectedSlotIndex].SelectorButton);
        }
    }

    public void RemoveItemFromInventory(InventorySlot inventorySlot)
    {
        _focusedInventory.RemoveEmptyInventoryItemFromPocket(inventorySlot.InventoryItem);

        // Check if the pocket is now empty after removing item from it, if so disable the pocket button icon in the catagory list.
        if (_focusedInventory.InventoryTypePockets[_pocketSelectorGroup.selectedIndex].Count == 0)
        {
            _pocketSelectorGroup.transform.GetChild(_pocketSelectorGroup.selectedIndex).gameObject.SetActive(false);
        }

        PoolInventorySlot(inventorySlot);
    }

    public void PoolInventorySlot(InventorySlot inventorySlot)
    {
        inventorySlot.transform.SetParent(_pooledInventoryHolder);
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