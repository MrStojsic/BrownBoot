using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=up6HcYph_bo")]
//- Updated in https://www.youtube.com/watch?v=EWJSu1kdIEc.
public class QuestGoal
{
    
    public Quest RelatedQuest { get; set; } //- The quest this goal is a goal for.
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Initialise()
    {
        // Default inti stuff here.
        
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        IsCompleted = true;
        RelatedQuest.CheckGoals(); //- when this goal is completed tell the related quest to check through all its goals to see if the quest is now complete.
    }

}
