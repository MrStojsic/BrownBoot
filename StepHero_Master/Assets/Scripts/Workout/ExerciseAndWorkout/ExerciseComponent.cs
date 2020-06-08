using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ExerciseComponent : MonoBehaviour
{

    public ExerciseData exerciseData;

    // UI Related Variables.
    [SerializeField] private Text titleText;
    [SerializeField] private Image iconImage;
     public Sprite iconSprite = null;

    public SimpleButton simpleButton;

    private void Start()
    {
        SetUi(); 
    }

    private void SetUi()
    {
        titleText.text = exerciseData.exerciseName;
        iconImage.color = ExerciseManager.instance.exerciseTypeColoursArray[(int)exerciseData.exerciseType];

        iconImage.sprite = iconSprite;
    }

    public void DeleteExercise()
    {
        ExerciseManager.instance.DeleteExercise(exerciseData.id, exerciseData.isCustom);
        Destroy(this.gameObject);
    }

    public void PassWorkoutDetails()
    {
        WorkoutManager.instance.SetWorkoutCreatePanel(exerciseData);

    }

}
