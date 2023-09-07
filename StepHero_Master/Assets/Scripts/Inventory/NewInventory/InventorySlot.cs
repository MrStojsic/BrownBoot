using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    // DATA.
    [SerializeField]
    protected InventoryItem _inventoryItem;
    public virtual InventoryItem InventoryItem
    {
        get { return _inventoryItem; }
        protected set
        {
            if (value != null)
            {
                _inventoryItem = value;
                _title.text = value.Item.Title;
                _icon.sprite = value.Item.Icon;
                _inventoryItem.InventorySlot = this;
            }
            else
            {
                gameObject.SetActive(false);
                InventoryUiManager.Instance.PoolSlot(this);
            }
        }
    }

    public virtual void UpdateStackSizeUI()
    {
        if (_inventoryItem != null)
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
                InventoryItem = null;
                _stackSizeText.color = Color.clear;
            }
        }
    }

    [SerializeField] protected Text _stackSizeText = default;
    public Text StackSizeText
    {
        get { return _stackSizeText; }
    }

    public void Initialise(InventoryItem inventoryItem, int index)
    {
        if (_inventoryItem != null)
        {
            _inventoryItem.InventorySlot = null;
        }
        InventoryItem = inventoryItem;
        Index = index;
    }
}



[System.Serializable]
public class InventoryItem
{
    [SerializeField] protected Item _item;
    public Item Item
    {
        get { return _item; }
    }

    [SerializeField]
    protected int _numberOfItem;
    public int NumberOfItem
    {
        get { return _numberOfItem; }
        protected set
        {
            if (_numberOfItem <= Item.StackSize)
            {
                _numberOfItem = value;

                _inventorySlot?.UpdateStackSizeUI();

                return;
            }
            Debug.LogWarning("Possible Mistake - " + value + " is more than item.StackSize of " + Item.StackSize + " or equal to current " + _numberOfItem);
        }
    }

    [SerializeField] protected InventorySlot _inventorySlot;
    public InventorySlot InventorySlot
    {
        get { return _inventorySlot; }
        set {
                _inventorySlot = value;

            _inventorySlot?.UpdateStackSizeUI();
        }
    }

    public InventoryItem(Item item, int numberOfItem, InventorySlot inventorySlot = null)
    {
        _item = item;
        InventorySlot = inventorySlot;
        if (numberOfItem > _item.StackSize)
        {
            Debug.LogError(item.Title + " - Attempted to make InventoryItem larger than stackSize.");
        }
        NumberOfItem = numberOfItem;
    }

    public virtual bool ReceiveItem(Item item, int amountToTransfer)
    {
        if (item == _item && amountToTransfer + _numberOfItem <= Item.StackSize)
        {
            NumberOfItem += amountToTransfer;
            return true;
        }
        return false;
    }

    public bool RemoveItems(int numberToRemove)
    {
        if (numberToRemove <= NumberOfItem)
        { 
            NumberOfItem -= numberToRemove;
            return true;
        }
        return false;
    }
    /// <summary>
    /// NOTE - Not to be used outside of PlayerInventory.
    /// </summary>
    /// <returns></returns>
    public virtual bool UseItem()
    {
        if (_item is IUseable && ((IUseable)_item).Use())
        {
            RemoveItems(1);
            return true;
        }
        return false;
    }
}



/*
[System.Serializable]
public class InventoryEquipableItem
{
    [SerializeField] protected Item _item;
    public Item Item
    {
        get { return _item; }
    }

    [SerializeField]
    public int NumberOfItem
    {
        get { return storedItemsDurabilities.Count; }
    }

    [SerializeField] private List<int> storedItemsDurabilities = new List<int>();
    public List<int> StoredItemsDurabilities
    {
        get { return storedItemsDurabilities; }
    }

    [SerializeField] protected InventorySlot _inventorySlot;
    public InventorySlot InventorySlot
    {
        get { return _inventorySlot; }
        set
        {
            _inventorySlot = value;

            _inventorySlot?.UpdateStackSizeUI();
        }
    }

   

    [SerializeField] private int focusedIndex = 0;
    public int FocusedIndex { get => focusedIndex; set => focusedIndex = value; }




    public InventoryEquipableItem(Item item, List<int>storedItemsDurabilities, InventorySlot inventorySlot = null)
    {
        _item = item;
        InventorySlot = inventorySlot;

        for (int i = 0; i < storedItemsDurabilities.Count; i++)
        {
            this.storedItemsDurabilities.Add(storedItemsDurabilities[i]);
        }
    }

    public virtual bool AddFromInventoryItem(InventoryEquipableItem sourceInventoryEquipableItem, int index)
    {
        if (sourceInventoryEquipableItem.Item == Item && 1 + NumberOfItem <= Item.StackSize && sourceInventoryEquipableItem.NumberOfItem >= index)
        {
            this.storedItemsDurabilities.Add(sourceInventoryEquipableItem.StoredItemsDurabilities[index]);

            sourceInventoryEquipableItem.RemoveItems(index);
            return true;
        }
        return false;
    }

    public void RemoveItems(int indexToRemove)
    {
        if (indexToRemove <= NumberOfItem)
        {
            storedItemsDurabilities.RemoveAt(indexToRemove);
        }
    }

    public virtual bool Interact()
    {
        if (_item is IUseable && ((IUseable)_item).Use())
        {
            //RemoveItems(1);
            return true;
        }
        return false;
    }
}*/