using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[HelpURL("https://www.youtube.com/watch?v=LONrh-6xbXQ")]
public class ReachNumberQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Reach Number";
        QuestDescription = "Press 'I' 5 times";
        //- Impliment either of the 2 options below.
        // ItemReward = // ITEM.
        // itemReward = ItemDatabase.Instance.GetItem(ID 10 number OR Sting value "Potion" for example);
        GoldReward = 100;

        //- Pass in this quest as the related quest, the description, isCompleted is false,
        //  the currentAmount is 0 or can be loaded in from a save maybe or checked if weve alread met it,
        //  and lastely the amountRequired to complete the goal.
        Goals.Add(new QuestTestGoal_TEST(this,QuestDescription, false,0,5));

        Goals.ForEach(g => g.Initialise());
    }

}
