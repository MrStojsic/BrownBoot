using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=hLggPX0ir5M&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=99")]
// Added Objectives in https://www.youtube.com/watch?v=FLXzG-bGHOA&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=101
public class ISS_QuestLog : UiWindow
{
    private static ISS_QuestLog _instance;
    public static ISS_QuestLog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ISS_QuestLog>();
            }
            return _instance;
        }
    }

    [SerializeField] private ISS_Quest _selectedQuest;

	[SerializeField]
    private QuestTypePocket[] _questTypePockets = new QuestTypePocket[5];
    public QuestTypePocket[] QuestTypePockets
    {
        get => _questTypePockets;
        protected set => _questTypePockets = value;
    }


	public void AcceptQuest(ISS_Quest quest)
    {
        if (!QuestTypePockets[quest.questTypeInt].IsFull)
        {
            foreach (CollectItemObjective o in quest.CollectionObjectives)
            {
                PlayerInventory.Instance.itemCountChanged += new ItemCountChanged(o.UpdateCollectedItemCount);
                o.UpdateCollectedItemCount();
            }

            QuestTypePockets[quest.questTypeInt].storedQuests.Add(quest);

            //- Initailse QuestLogUiManager just in case we need to reference the slots or displayDetailSlot before opening the QuestLog.
            QuestLogUiManager.Instance.InitaliseSlotPockets();
			// TODO
			// REMEMBER TO CHECK IF NEW QUESTS OBJECTIVES ARE ALREADY COMPLETED AT TIME OF ACCEPTING.
        }

    }


    public void AbandonQuest()
    {
        //- Remember if we go with dialoge based quests
        //  this needs to undo that dialoge making the quest availible again.

        foreach (CollectItemObjective o in _selectedQuest.CollectionObjectives)
		{
            PlayerInventory.Instance.itemCountChanged -= new ItemCountChanged(o.UpdateCollectedItemCount);
        }
        RemoveQuest(_selectedQuest);
    }
    public void RemoveQuest(ISS_Quest quest)
    {
        QuestTypePockets[quest.questTypeInt].storedQuests.Remove(quest);
        _selectedQuest = null;
		quest.questScript.Quest.questGiver.UpdateQuestStatus();
		// UPDATE QUEST SLOT UI AS WE REMOVED A QUEST.
    }
    //- Takes in a quest and checks our list of accepted quests to see if we the quest has already been accepted.
    public bool HasQuest(ISS_Quest quest)
    {
        //- Check if the quest exists in the list of accepted quests.
        return QuestTypePockets[quest.questTypeInt].storedQuests.Exists(x => x.Title == quest.Title);
    }

}
