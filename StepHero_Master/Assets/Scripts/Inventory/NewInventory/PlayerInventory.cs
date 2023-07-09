using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//- Added ItemCountDelegate and Event from https://www.youtube.com/watch?v=7eOFqwGH_hA&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=64
//  Still want to update the way it works after updatig the player inventory. 
public delegate void ItemCountChanged(Item item);

public class PlayerInventory : Inventory
{
    private static PlayerInventory _instance;
    public static PlayerInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInventory>();
            }
            return _instance;
        }
    }

    public event ItemCountChanged itemCountChanged;

    public override bool AttemptAddItem(Item item, int amountToReceive)
    {
        if (base.AttemptAddItem(item, amountToReceive) == true)
        {
            OnItemCountChanged(item);
            return true;
        }
        return false;
    }

    public void RemoveItems(InventoryItem inventoryItem, int numberToRemove)
    {
        if (inventoryItem.RemoveItems(numberToRemove))
        {
            OnItemCountChanged(inventoryItem.Item);
        }
    }

    public void UseItem(InventoryItem inventoryItem)
    {
        if (inventoryItem.UseItem())
        {
            itemCountChanged.Invoke(inventoryItem.Item);
        }
    }

    //- WE need to check that the event itemCountChanged has some listeners before we call it otherwise we will get a null ref error.
    public void OnItemCountChanged(Item item)
    {
        if (itemCountChanged != null)
        {
            itemCountChanged.Invoke(item);
        }
    }


}
