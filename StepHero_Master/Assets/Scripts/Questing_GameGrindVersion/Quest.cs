﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[HelpURL("https://www.youtube.com/watch?v=jN-27UawCgU")]
public class Quest : MonoBehaviour
{
    public List<QuestGoal> Goals { get; set; } = new List<QuestGoal>();
    public string QuestName { get; set; }
    public string QuestDescription { get; set; }
    public int GoldReward { get; set; }
    //- Yet to impliment or consider how it works with training EXP.
    public int ExpReward { get; set; }
    //public InventoryItem ItemReward { get; set; }
    public bool IsCompleted { get; set; }
    

    public void CheckGoals()
    {
        //- This uses the Linq library All, this checks all the entries of a list and checks a specified variable in them to see if they all meet a condition.
        //  in this case if all the QuestGoals in Goal IsComplete all equal true, in which case all of the goals have been completed so this quest should also be completed.
        if (Goals.All(g => g.IsCompleted == true))
        {
            IsCompleted = true;
            // TODO - I want the player to need to walk back to the quest giver or report that is been completed,
            //        rather than the reward just pop into existance, so we will call give reward later.
            //GiveReward();
        }

    }

    public bool GiveReward()
    { 
        // TODO
        //- Watch GameGrind RPG series to see how he implimented his inventory controller
        //  and try to merge ours with his so this works.
        /*
        if (ItemReward != null)
        {
            // Perhaps use item ID when we get to this?
            InventoryController.Instance.GiveItem(ItemReward);
        }*/
        if (GoldReward > 0)
        {
            // Give Player goldReward amount of gold.
        }

        //- We need to check if the player was able to accept the reward and return the outcome,
        //  otherwise the player may speak to the guest giver and not be able to take the reward
        //  and then the quest giver will never reward them,
        //   we need the player to be able to speak to them again later if they couldnt take the reward the first time.
        // DEBUG.
        return true;
    }


}
