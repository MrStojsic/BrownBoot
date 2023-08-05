using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[HelpURL("https://www.youtube.com/watch?v=bf-xP0YYlx8&list=PLX-uZVK_0K_6JEecbu3Y-nVnANJznCzix&index=106")]
public class ISS_QuestGiverWindow : NpcWindow
{
    // - This class isnt used anywhere yet, its more of an idea of how to access the quests in the quest giver.
    //   I wanted to go with a more skyrim style quest system where you speak to a person and they have speech options
    //   that lead to quests, rather than a list of quests to choose from.
    private ISS_QuestGiver _questGiver;

    public void ShowQuest(ISS_QuestGiver questGiver)
    {
        _questGiver = questGiver;

        foreach (ISS_Quest quest in questGiver.Quests)
        {
            // access and show quests.
        }
    }

    public override void Show(IInteractable interactable)
    {
        ShowQuest(interactable as ISS_QuestGiver);
        base.Show();
    }
}
