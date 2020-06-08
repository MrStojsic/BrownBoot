using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutComponent : MonoBehaviour
{
    public WorkoutData workoutData;

    // UI Related Variables.
    [SerializeField] private Text titleText;
    [SerializeField] private Text timeCompletedText;
    [SerializeField] private Text durationText;
    [SerializeField] private Image iconImage;

    public SimpleButton simpleButton;

    private void Start()
    {
        SetUi();
    }

    public void SetUi()
    {
        ExerciseData exerciseData = workoutData.isCustom ?
            ExerciseManager.instance.customExerciseDataDictionary[workoutData.id] :
            ExerciseManager.instance.defaultExerciseDataList[workoutData.id];

        titleText.text = exerciseData.exerciseName;

        timeCompletedText.text = workoutData.completedTime.ToShortDateString();

        durationText.text = string.Format("{0:D2}h {1:D2}m", workoutData.duration.Hours, workoutData.duration.Minutes);

        iconImage.color = ExerciseManager.instance.exerciseTypeColoursArray[(int)exerciseData.exerciseType];

        iconImage.sprite = ExerciseManager.instance.spriteArray[exerciseData.iconIndex];
    }

    public void DeleteWorkout()
    {
        WorkoutManager.instance.RemoveWorkout(workoutData);
        gameObject.SetActive(false);
    }
}
