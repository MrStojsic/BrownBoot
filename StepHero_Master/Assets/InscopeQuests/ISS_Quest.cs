using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]
[System.Serializable]
public class ISS_Quest
{
    [SerializeField]
    private string title;
    public string Title { get => title; set => title = value; }

    [SerializeField]
    private string _description;
    public string Description { get => _description; set => _description = value; }

    [SerializeField]
    private CollectionObjective[] _collectionObjectives;
    public CollectionObjective[] CollectionObjectives { get => _collectionObjectives; }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int _amount;
    public int Amount { get => _amount; }
    [SerializeField]
    private int _currentAmount;
    public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

    //- Diviated from tutorial to store an item rather than the string name for comparisons,
    //  but we stll compare to the items name. may use item IDs in future.
    [SerializeField]
    private Item _item;
    public Item Item { get => _item; set => _item = value; }


}

[System.Serializable]
public class CollectionObjective : Objective
{
    public void UpdateCollectedItemCount(Item item)
    {
        //- We compare to the items name. may use item IDs in future.
        if (Item.Title == item.Title)
        {
            CurrentAmount = InventoryPageManager.Instance.PlayerInventory.InventoryTypePockets[(int)item.ItemType].FindItem(item).NumberOfItem;
            Debug.Log("Quest - CurrentAmount = " + CurrentAmount);
        }
    }
}
