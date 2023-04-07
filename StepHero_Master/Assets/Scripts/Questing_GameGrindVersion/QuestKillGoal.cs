using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=h7rRic4Xoak")]
public class QuestKillGoal : QuestGoal
{
    public int EnemyID { get; set; }

    public QuestKillGoal(int enemyID, string description, bool isCompleted, int currentAmount, int requiredAmount)
    {
        this.EnemyID = enemyID;
        this.Description = description;
        this.IsCompleted = isCompleted;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Initialise()
    {
        base.Initialise();
        // TODO
        // This hasnt been implimented, watch game grind RPG series for implimentation.
        //CombatEvents.OnEnemyDeath += EnemyDied;
    }

    void EnemyDied(Enemy enemy)
    {
        if (enemy.ID == this.EnemyID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }


}