﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=bf-xP0YYlx8&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=106")]
public class ISS_QuestGiverWindow : NpcWindow
{
    // - This class isnt used anywhere yet, its more of an idea of how to access the quests in the quest giver.
    //   I wanted to go with a more skyrim style quest system where you speak to a person and they have speech options
    //   that lead to quests, rather than a list of quests to choose from.
    private ISS_QuestGiver _questGiver;

    // NOTE: NOT IMPLIMENTED YET!!
    private ISS_Quest _currentQuest;



    public void ShowQuest(ISS_QuestGiver questGiver)
    {
        _questGiver = questGiver;

        foreach (ISS_Quest quest in questGiver.Quests)
        {
            // -Access and show quests.
            // -Remember to check for which quests have been completed too, not just quests we have.
        }
    }

    public override void Show(IInteractable interactable)
    {
        ShowQuest(interactable as ISS_QuestGiver);
        base.Show();
    }

    // NOTE: Im putting this Complete quest fucntion in here
    //       but later it can be in the Questgiver class or handled outside of the QL if wanted.
    //- If the current quest is complete, we want to go throught the quest givers quests and null out that index,
    //  removing the quest from their array of quests.
    //- Eventually this will be handled by the dialogue system as a dialogue progression.
    public void CompleteQuest()
    {
        if (_currentQuest.IsComplete)
        {
            for (int i = 0; i < _questGiver.Quests.Length; i++)
            {
                if (_currentQuest == _questGiver.Quests[i])
                {
                    _questGiver.CompletedQuests.Add(_currentQuest.Title);
                    _questGiver.Quests[i] = null;
                    _currentQuest.questGiver.UpdateQuestStatus();
                }
            }

			foreach (CollectItemObjective o in _currentQuest.CollectionObjectives)
			{
                PlayerInventory.Instance.itemCountChanged -= new ItemCountChanged(o.UpdateCollectedItemCount);
                o.Complete();
            }

            ISS_QuestLog.Instance.RemoveQuest(_currentQuest);
        }
    }
}
