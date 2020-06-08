using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=h7rRic4Xoak")]
public class QuestKillGoal : QuestGoal
{
    public int EnemyID { get; set; }

    public QuestKillGoal(int enemyID, string description, bool isCompleted, int progressAmount, int requiredAmount)
    {
        this.EnemyID = enemyID;
        this.Description = description;
        this.IsCompleted = isCompleted;
        this.ProgressAmount = progressAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Initialise()
    {
        base.Initialise();
    }

    // TODO - cehck video.
    void EnemyDied(Enemy enemy)
    {
        if (enemy.id == this.EnemyID)
        {
            this.ProgressAmount++;
            Evaluate();
        }
    }


}

public struct Enemy { public int id; }