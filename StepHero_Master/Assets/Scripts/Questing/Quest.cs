using System.Collections;
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
    //public InventoryItem ItemReward { get; set; }
    public bool IsCompleted { get; set; }

    public void CheckGoals()
    {
        //- This uses the Linq library All, this checks all the entries of a list and checks a specified variable in them to see if they all meet a condition.
        //  in this case if all the QuestGoals in Goal IsComplete all equal true, in which case all of the goals have been completed so this quest should also be completed.
        if (Goals.All(g => g.IsCompleted == true))
        {
            IsCompleted = true;

            GiveReward();
        }

    }

    void GiveReward()
    { 
        // TODO
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
    }


}
