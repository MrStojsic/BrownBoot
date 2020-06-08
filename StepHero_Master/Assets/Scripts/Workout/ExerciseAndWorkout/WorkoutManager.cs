using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorkoutManager : MonoBehaviour
{

    [SerializeField] int lenghtLimit = 100;

    public CreateWorkout createWorkout;

    //- this will need a function called to check if workouts are listed as quests at some point.

    public List<WorkoutData> workoutList = new List<WorkoutData>();

    public static WorkoutManager instance = null;

    private void Awake()
    {
        CreateInstance();

    }

    private void Start()
    {
        Load();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

    }


    public void SetWorkoutCreatePanel(ExerciseData exerciseData)
    {
        if (createWorkout != null)
        {
            createWorkout.SetExerciseToDisplay(exerciseData);
        }
    }


    public WorkoutData CreateNewWorkout(bool _isCustom, int _id, int _hours, int _mins)
    {
        //- this will need a function called to check if workouts are listed as quests at some point.
        WorkoutData workoutData = new WorkoutData(_isCustom, _id, _hours, _mins);
        workoutList.Add(workoutData);

        TrimListToLimit();

        Save();
        return workoutData;

    }

    private void TrimListToLimit()
    {
        if (workoutList.Count > lenghtLimit)
        {
            workoutList.RemoveAt(0);
        }
    }

    public void RemoveWorkoutsOfId(int _id, bool _isCustom)
    {
        //- You cannot remove an item from a List that you are iterating over with foreach of looping forwards through
        //  without erros, so here we for loop backwards backwards using RemoveAt(i).
        for (int i = workoutList.Count-1; i > 0; i--)
        {
            if (workoutList[i].id == _id && workoutList[i].isCustom == _isCustom)
            {
                //Debug.Log(i);
                workoutList.RemoveAt(i);
   
                createWorkout.workoutCatalogueManager.DisableChildAtIndex(i);
            }
        }
        Save();
        
    }
 
    public void RemoveWorkout(WorkoutData _workoutData)
    {
        ExerciseManager.instance.UpdateExerciseTotalDurationTrained(_workoutData.isCustom, _workoutData.id, -_workoutData.duration.Hours, -_workoutData.duration.Minutes);
        workoutList.Remove(_workoutData);
        Save();
    }


    public void Save()
    {
        SaveSystem.SaveWorkoutData(workoutList);
    }
    public void Load()
    {
        workoutList = SaveSystem.LoadWorkoutData();
        if (workoutList == null)
        {
            workoutList = new List<WorkoutData>();
        }
        ExerciseData exerciseData;
        foreach (var workoutData in workoutList)
        {
            if (workoutData.isCustom)
            {
                exerciseData = ExerciseManager.instance.customExerciseDataDictionary[workoutData.id];
            }
            else
            {
                exerciseData = ExerciseManager.instance.defaultExerciseDataList[workoutData.id];
            }
        }
    }
}

