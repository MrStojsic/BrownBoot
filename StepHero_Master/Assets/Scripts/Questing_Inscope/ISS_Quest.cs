using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestType
{
    MAIN,       // 0
    SUB,		// 1
    HUNT,		// 2
    TREASURE,	// 3
	COMPLETED,	// 4
}

[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]
[System.Serializable]
public class ISS_Quest
{
    [SerializeField]
    private string _title;
    public string Title { get => _title; set => _title = value; }

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get => _icon; set => _icon = value; }

    [SerializeField]
    private QuestType _questType;
	public QuestType QuestType { get => _questType; }

    [TextArea] [SerializeField] private string _shortDescription = default;
    public string ShortDescription
    { get => _shortDescription; }

    [TextArea] [SerializeField] private string _longDescription = default;
    public string LongDescription
    { get => _longDescription; }


    [SerializeField]
    private CollectItemObjective[] _collectionObjectives = null;
    public CollectItemObjective[] CollectionObjectives { get => _collectionObjectives; }

    public ISS_QuestScript questScript;
    public ISS_QuestGiver questGiver;

    public bool IsComplete
    {
        get
        {
            foreach (Objective o in CollectionObjectives)
            {
                if (!o.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }

	public int questTypeInt { get => (int)_questType; }
}

[System.Serializable]
public abstract class Objective
{
    [TextArea] [SerializeField] private string _description = default;
    public string Description
    { get => _description; }

    [SerializeField]
    private int _amount = default;
    public int Amount { get => _amount; }
    [SerializeField]
    private int _currentAmount = default;
    public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

    //- Diviated from tutorial to store an item rather than the string name for comparisons,
    //  but we stll compare to the items name. may use item IDs in future.
    [SerializeField]
    private Item _item = default;
    public Item Item { get => _item; set => _item = value; }

    public bool IsComplete
    {
        get
        {
            return CurrentAmount >= Amount;
        }
    }

}

//- This CollectItemObjective is to collect an item but you can keep the item,
//  can be used for teaching to craft items etc.
//  We can also make a FetchItemObjective to bring an item for some one and they will take the item from you upon completion.
[System.Serializable]
public class CollectItemObjective : Objective
{
	//- This one is called when we update the number of the collection item once weve already accepted the quest.
    public void UpdateCollectedItemCount(Item item)
    {
        //- We compare to the items name. may use item IDs in future.
        if (Item.Title == item.Title)
        {
            CurrentAmount = PlayerInventory.Instance.FindItem(item).NumberOfItem;
            QuestLogUiManager.Instance.UpdateDisplayedQuest();
			// NEEDS UPDATING WITH NEW SLOT SYSTEM
           // ISS_QuestLog.Instance.CheckCompletion();
        }
    }

	//- This one is only called when accepting the quest to check if we already have any of the required items.
    public void UpdateCollectedItemCount()
    {
        CurrentAmount = PlayerInventory.Instance.FindNumerOfItem(Item);

        // NEEDS UPDATING WITH NEW SLOT SYSTEM
        //ISS_QuestLog.Instance.CheckCompletion();
    }

    public void Complete()
    {
		//- Handle completion of quest here.

		//- EG remove items from iventory for fetch quests.
	}
}
