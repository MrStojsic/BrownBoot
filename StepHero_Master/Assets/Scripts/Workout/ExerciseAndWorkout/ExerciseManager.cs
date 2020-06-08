using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExerciseManager : MonoBehaviour
{

    public List<ExerciseData> defaultExerciseDataList = new List<ExerciseData>();
    public Dictionary<int, ExerciseData> customExerciseDataDictionary = null;

    public Sprite[] spriteArray;
    public Color[] exerciseTypeColoursArray;

    public static ExerciseManager instance = null;



    private void Awake()
    {
        CreateInstance();
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

    // vvvvvvvv TESTING.
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {

        }
    }
    // ^^^^^^^^ TESTING.

    public void UpdateExerciseTotalDurationTrained(bool _isCustom, int _id, int _hours, int _mins)
    {
        TimeSpan timeTrained;
        timeTrained += TimeSpan.FromHours(_hours);
        timeTrained += TimeSpan.FromMinutes(_mins);

        if (_isCustom)
        {
            customExerciseDataDictionary[_id].totalDurationTrained += timeTrained;
            customExerciseDataDictionary[_id].totalSessionsTrained += timeTrained.Ticks > 0 ? 1 : -1;
            print(customExerciseDataDictionary[_id].totalSessionsTrained);
        }
        else
        {
            defaultExerciseDataList[_id].totalDurationTrained += timeTrained;
            defaultExerciseDataList[_id].totalSessionsTrained += timeTrained.Ticks > 0 ? 1 : -1;
            print(defaultExerciseDataList[_id].totalSessionsTrained);
        }

        Save();
    }

    public ExerciseData CreateNewExercise(string _exerciseName, int _exerciseType, int _iconIndex)
    {
        int id = GenerateNewExerciseId();
        ExerciseData exerciseData =  new ExerciseData(_exerciseName, _exerciseType, _iconIndex, true, id);
        customExerciseDataDictionary.Add(id ,exerciseData);
        Save();
        return exerciseData;
    }

    public void DeleteExercise(int _id, bool _isCustom)
    {
        //- FLAG + this will need a function called to check if exercises are linked to an quests, and if so delete warn and delete that quest.
        //  EG, you currently have a quest linked to this exercise, if you delete wish to remove this exercise you will fail the quest.
        WorkoutManager.instance.RemoveWorkoutsOfId(_id, _isCustom);

        customExerciseDataDictionary.Remove(_id);

        Save();
    }

    public int GenerateNewExerciseId()
    {
        int id = 0;

        if (customExerciseDataDictionary.Count > 0)
        {
            List<int> keys = new List<int>(customExerciseDataDictionary.Keys);
            foreach (int key in keys)
            {
                if (key > id)
                {
                    id = key;
                }
            }
            id++;
        }
        return id;
    }

    public void Save()
    {
        SaveSystem.SaveCustomExerciseData(customExerciseDataDictionary);

        List<TimeSpan> defaultExerciseTotalTimeTrainedList = new List<TimeSpan>();
        for (int i = 0; i < defaultExerciseDataList.Count; i++)
        {
            defaultExerciseTotalTimeTrainedList.Add(defaultExerciseDataList[i].totalDurationTrained);
        }
        SaveSystem.SaveDefaultExerciseTotalTime(defaultExerciseTotalTimeTrainedList);
    }

    public void Load()
    {
        customExerciseDataDictionary = SaveSystem.LoadCustomExerciseData();
        if (customExerciseDataDictionary == null)
        {
            customExerciseDataDictionary = new Dictionary<int, ExerciseData>();
            Save();
        }
        List<TimeSpan> defaultExerciseTotalTimeTrainedList = SaveSystem.LoadDefaultExerciseTotalTime();
        if (defaultExerciseTotalTimeTrainedList != null)
        {
            for (int i = 0; i < defaultExerciseDataList.Count; i++)
            {
                defaultExerciseDataList[i].totalDurationTrained = defaultExerciseTotalTimeTrainedList[i];
            }
        }
    }



}

/*
 *  STR
 *  strength training
 *  cross fit
 *  core workout
 *  power lifting

 *  AGI
 *  running
 *  walikng
 *  cycling
 *  elliptical
 *  stair master
 *  HIIT
 *  rowing
 *  stretching
 *  hiking
 *  swimming
 
 *  INT
 *  yoga
 *  pilates
 *  meditation
 *  fishing
 *  tai chi
 *  qi gong
 */
