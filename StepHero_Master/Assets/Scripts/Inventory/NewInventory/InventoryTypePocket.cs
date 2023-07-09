using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  InventoryTypePocket
{
    [SerializeField]
    public List<InventoryItem> storedItems;

    public void Initialise(ItemType itemType)
    {
        storedItems = new List<InventoryItem>();
        _pocketsItemType = itemType;
    }

    private int _sizeLimit = 1000;
    /// <summary>
    /// This is the limit of how many different items can be stored in the pocket,
    /// By default this is set to 1000 for Shops, but this should be set to the limit for the player when loaded.
    /// </summary>
    public int SizeLimit
    {
        get
        {
            return _sizeLimit;
        }
        set
        {
            _sizeLimit = value;
        }
    }

    public int Count
    {
        get { return storedItems.Count; }
    }

    public bool IsEmpty
    {
        get { return storedItems.Count == 0; }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || Count < _sizeLimit)
            {
                return false;
            }
            return true;
        }
    }

    private ItemType _pocketsItemType;
    public ItemType PocketsItemType
    {
        get => _pocketsItemType;
    }

    // TODO - This could also be optimised by using item IDs and passing na int ID and comparing it to the storedItems ids.
    public InventoryItem FindItem(Item item)
    {
        int pocketIndex = item.GetItemTypeInt();

        for (int i = 0; i < storedItems.Count; i++)
        {
            if (item == storedItems[i].Item)
            {
                return storedItems[i];
            }
        }

        return null;
    }
}