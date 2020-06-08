using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=up6HcYph_bo")]
public class QuestGoal
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int ProgressAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Initialise()
    {
        // Default inti stuff here.
        
    }

    public void Evaluate()
    {
        if (ProgressAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        IsCompleted = true;
    }

}
