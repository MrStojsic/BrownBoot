using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=jN-27UawCgU")]
public class QuestTestGoal_TEST : QuestGoal
{

    public QuestTestGoal_TEST(string description, bool isCompleted, int progressAmount, int requiredAmount)
    {
        this.Description = description;
        this.IsCompleted = isCompleted;
        this.ProgressAmount = progressAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Initialise()
    {
        base.Initialise();
    }

    public void IncrementProgress()
    {
        this.ProgressAmount++;
        Evaluate();
    }


}
