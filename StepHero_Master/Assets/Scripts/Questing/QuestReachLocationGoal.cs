using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=h7rRic4Xoak")] // Based off it.
public class QuestReachLocationGoal : QuestGoal
{
    public int NodeID { get; set; }

    public QuestReachLocationGoal(int nodeID, string description, bool isCompleted, int progressAmount, int requiredAmount)
    {
        this.NodeID = nodeID;
        this.Description = description;
        this.IsCompleted = isCompleted;
        this.ProgressAmount = progressAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Initialise()
    {
        base.Initialise();
    }

    void ReachedNewNode(AStarNode node)
    {
        if (node.id == this.NodeID)
        {
            this.ProgressAmount++;
            Evaluate();
        }
    }


}

