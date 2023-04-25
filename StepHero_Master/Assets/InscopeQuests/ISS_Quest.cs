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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
    [SerializeField]
    private string _type;
    public string Type { get => _type; set => _type = value; }
}

[System.Serializable]
public class CollectionObjective : Objective
{
    public void UpdateCollectedItemCount(Item item)
    {
        //- We compare the name of the items after comparing to lower case just in case there are any casing errors.
        if (Type.ToLower() == item.Title.ToLower())
        {
            //- Stopped here as "itemCountChangedEvent" isnt implimented yet and the totorial was about to use it.
            https://youtu.be/lbJsitsHcUs?t=178

            CurrentAmount = InventoryPageManager.Instance.PlayerInventory.InventoryTypePockets[(int)item.ItemType].FindItem(item).NumberOfItem;
            Debug.Log("Quest - CurrentAmount = " + CurrentAmount);
        }
    }
}
