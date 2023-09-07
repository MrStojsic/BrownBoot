using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiManager : SlotUiManager
{
	private static InventoryUiManager _instance;
	public static InventoryUiManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<InventoryUiManager>();
			}
			return _instance;
		}
	}

	// _focusedInventoryPockets is the inventory we are currently displaying, this can be the player, shop or loot depending what we need to be displayed in the UI.
	private Inventory _focusedInventory = default;

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
			PlayerInventory.Instance.AttemptAddItem(testItemToAdd.Item, 1);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			Debug.Log("Pressed D - Deloaded mechant inventory.");
			NonPlayerInventory = null;
			ChangeFocusedInventory(true);
		}

	}
	// TOHERE

	public void SetNonPlayerInventory(Inventory inventory)
	{
		_nonPlayerInventory = inventory;
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
			_focusedInventory = PlayerInventory.Instance;
			// - The 'is' operator in C# is used to check the object type and it returns a bool value: true if the object is the same type and false if not.
			//   So if _nonPlayerInventory is a merchant, pass PLAYER_SELL, otherwise pass PLAYER_USE as the parameter in SetInteractionInterface.
			(_displayDetailSlot as DisplayItemDetail).SetInteractionInterface(_nonPlayerInventory is Merchant_InventoryManager ? InteractionType.PLAYER_SELL : InteractionType.PLAYER_USE);
		}
		else
		{
			_focusedInventory = _nonPlayerInventory;
			(_displayDetailSlot as DisplayItemDetail).SetInteractionInterface(_nonPlayerInventory is Merchant_InventoryManager ? InteractionType.SHOP_BUY : InteractionType.LOOT_TAKE);
		}
		slotPockets = _focusedInventory.InventoryTypePockets;
		InitialisePockets();
	}

	protected override void SetSpecializedSlotFunctionality(Slot newSlot, int slotIndex)
	{
		(newSlot as InventorySlot).Initialise(_focusedInventory.InventoryTypePockets[selectedPocketIndex].storedItems[slotIndex], slotIndex);
	}


	public override void ClearEmptySlot(Slot slot)
	{
		_focusedInventory.RemoveEmptyInventoryItemFromPocket((slot as InventorySlot).InventoryItem);
		base.ClearEmptySlot(slot);
	}

	// -The following 3 functions are used for sorting the items by name or value, its a WIP.
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