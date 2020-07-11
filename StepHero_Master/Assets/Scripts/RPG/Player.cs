using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// ========================= PLAYER =========================
public class Player : Entity
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }


    public DateTime saveTime;

    protected override void Start()
    {
        base.Start();
        //SavePlayer();

       // LoadPlayer();
    }

    
    protected override void Update()
    {
        // vvvvvvvvvvvvv DEBUG ONLY
        if (Input.GetKeyDown(KeyCode.D))
        {
            TestStat.MyCurrentValue -= 10;
            Debug.Log(TestStat.MyCurrentValue);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TestStat.MyCurrentValue += 10;
            Debug.Log(TestStat.MyCurrentValue);
        }
        // ^^^^^^^^^^^^ DEBUG ONLY
    }

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
            Level = playerData.level;
            base.Health = playerData.health;
            Title = playerData.title;
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
