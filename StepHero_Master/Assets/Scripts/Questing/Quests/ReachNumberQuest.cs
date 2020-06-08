using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachNumberQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Reach Number";
        QuestDescription = "Press 'I' 5 times";
        // ItemReward = // ITEM.
        GoldReward = 100;


        Goals.Add(new QuestTestGoal_TEST(QuestDescription, false,0,5));

        Goals.ForEach(g => g.Initialise());
    }

}
