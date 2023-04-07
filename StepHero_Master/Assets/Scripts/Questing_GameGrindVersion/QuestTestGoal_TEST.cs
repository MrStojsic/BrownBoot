using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=jN-27UawCgU")]
//- Added RelatedQuest in https://www.youtube.com/watch?v=EWJSu1kdIEc.
public class QuestTestGoal_TEST : QuestGoal
{
    public QuestTestGoal_TEST(Quest relatedQuest, string description, bool isCompleted, int currentAmount, int requiredAmount)
    {
        this. RelatedQuest = relatedQuest;
        this.Description = description;
        this.IsCompleted = isCompleted;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Initialise()
    {
        base.Initialise();
    }

    public void IncrementProgress()
    {
        this.CurrentAmount++;
        Evaluate();
    }


}
