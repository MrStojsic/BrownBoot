using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// ========================= PLAYER =========================
public class Player : Entity
{

    [SerializeField]
    private Stat testStat;
    [SerializeField]
    private float maxTestStat;

    public DateTime saveTime;

    protected override void Start()
    {
        //testStat.Initialize(maxTestStat, maxTestStat);

        //SavePlayer();

        LoadPlayer();
    }

    /*
    protected override void Update()
    {
        // vvvvvvvvvvvvv DEBUG ONLY
        if (Input.GetKeyDown(KeyCode.D))
        {
            testStat.MyCurrentValue -= 10;
            Debug.Log(testStat.MyCurrentValue);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            testStat.MyCurrentValue += 10;
            Debug.Log(testStat.MyCurrentValue);
        }
        // ^^^^^^^^^^^^ DEBUG ONLY
    }*/

    /*
 public void ConvertToExperience(int hours, int minutes, Workout.WorkoutType workoutType)
 {
     switch (workoutType)
     {
         case Workout.WorkoutType.STR:
             timeSpentTrainigStrength = hours + minutes / 60f;
             break;
         case Workout.WorkoutType.AGI:
             timeSpentTrainigAgility = hours + minutes / 60f;
             break;
         case Workout.WorkoutType.INT:
             timeSpentTrainigIntelligence = hours + minutes / 60f;
             break;
     }
     timeSpentTrainigStrength = hours + minutes / 60f;
     Debug.Log(timeSpentTrainigStrength);
 }
 */











    // ===========================================================================
    //- Here down is NOT from "In Scope Studios" tutorials.
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();


        if (playerData != null)
        {
            level = playerData.level;
            base.health = playerData.health;
            title = playerData.title;
            //- Convert saveTime from sting to long to DateTime.
            long saveTimeAsBinary = Convert.ToInt64(playerData.saveTime);
            saveTime = DateTime.FromBinary(saveTimeAsBinary);
        }
        else
        {
            Debug.LogError("PlayerData not found, do you wish to make a new player?");
        }
    }

    public void DeletePlayer()
    {
        SaveSystem.DeleteSaveFile(0);
    }

}
