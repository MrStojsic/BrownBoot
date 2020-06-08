using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ========================= EXERCISE DATA =========================
[Serializable]
public class ExerciseData
{
    public enum ExerciseType
    {
        STR = 0,
        AGI = 1,
        INT = 2
    };

    // Raw Exercise Data.
    public string exerciseName = null;
    public ExerciseType exerciseType = 0;
    public int iconIndex = 0;
    public bool isCustom = false;
    [HideInInspector]
    public int id = 0; // - This can act as the exercises ID, as so far this doent change once the exercise has been created and saved.

    public TimeSpan totalDurationTrained;
    public int totalSessionsTrained;

    public ExerciseData(string _exerciseName, int _exerciseType, int _iconIndex, bool _isCustom, int _id)
    {
        exerciseName = _exerciseName;
        exerciseType = (ExerciseType)_exerciseType;
        iconIndex = _iconIndex;
        isCustom = _isCustom;
        id = _id;
    }
}
