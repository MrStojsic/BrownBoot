using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateWorkout : MonoBehaviour
{
    private ExerciseData exerciseData;

    [SerializeField] private InputField hoursInput = default;
    [SerializeField] private InputField minsInput = default;
    int hours = default;
    int mins = default;
    [SerializeField] Color[] flashColours = default;

    [SerializeField] Text titleText = default;
    [SerializeField] Text totalTimeText = default;
    [SerializeField] Image iconImage = default;

    [SerializeField] GameObject incorrectEntryWindow = default;

    [SerializeField] SetActiveUiElement workoutEntryPanel = default;
    [SerializeField] SetActiveUiElement workoutCataloguePanel = default;

    [SerializeField] public  WorkoutCatalogueManager workoutCatalogueManager;


    public void SetExerciseToDisplay(ExerciseData _exerciseData)
    {
        // Exercise.
        exerciseData = _exerciseData;
        // Title.
        titleText.text = _exerciseData.exerciseName;
        // Total Time Trained.
        string totalTimeTrained = "";
        int days = _exerciseData.totalDurationTrained.Days;
        if (days > 7)
        {
            totalTimeTrained = ((int)(days/7)).ToString() + "w ";
            days = days % 7;
        }
        if (days > 0)
        {
            totalTimeTrained += days.ToString() + "d ";
        }
        totalTimeTrained += _exerciseData.totalDurationTrained.ToString("h'h 'm'm'");
        totalTimeText.text = totalTimeTrained;
        // Sprite.
        iconImage.sprite = ExerciseManager.instance.spriteArray[_exerciseData.iconIndex];
        iconImage.color = ExerciseManager.instance.exerciseTypeColoursArray[(int)_exerciseData.exerciseType];

        workoutEntryPanel.SetUiActive();

        // Time Input.
        ResetWorkoutEntry();
        print("This ran");
    }

    public void ValidateTimeInput()
    {
        if (Int32.TryParse(hoursInput.text, out hours))
        {
            hours = Int32.Parse(hoursInput.text);
        }
        else
        {
            hoursInput.text = "";
        }
        if (Int32.TryParse(minsInput.text, out mins))
        {
            mins = Int32.Parse(minsInput.text);
        }
        else
        {
            minsInput.text = "";
        }

        if (hoursInput.text.Length == 2)
        {
            minsInput.Select();
        }

        if (hours > 23)
        {
            hours = 23;
            hoursInput.text = "23";
            StartCoroutine(FlashTextBox(hoursInput));
        }
        if (mins > 59)
        {
            mins = 59;
            minsInput.text = "59";
            StartCoroutine(FlashTextBox(minsInput));
        }

        if (hours < 0)
        {
            hours = 0;
            hoursInput.text = "0";
            hoursInput.Select();
            StartCoroutine(FlashTextBox(hoursInput));

        }
        if (mins < 0)
        {
            mins = 0;
            minsInput.text = "0";
            minsInput.Select();
            StartCoroutine(FlashTextBox(minsInput));
        }
    }

    private IEnumerator FlashTextBox(InputField inputField)
    {
        inputField.image.color = flashColours[1];
        yield return new WaitForSeconds(0.075f);
        inputField.image.color = flashColours[0];
        yield return new WaitForSeconds(0.1f);
        inputField.image.color = flashColours[1];
        yield return new WaitForSeconds(0.075f);
        inputField.image.color = flashColours[0];
    }

    public void ResetWorkoutEntry()
    {
        hoursInput.text = null;
        minsInput.text = null;

        hoursInput.Select();
        hoursInput.ActivateInputField();
    }

    public void CreateWorkoutEntry()
    { 
        if ((hours*60 + mins) < 1)
        {
            StartCoroutine(FlashTextBox(minsInput));
            // incorrectEntryWindow will show workout must be longer than 1 min.
            return;
        }

        workoutCatalogueManager.EnterNewWorkoutAndAddToCalaloguePage(exerciseData.isCustom, exerciseData.id, hours, mins);

        ExerciseManager.instance.UpdateExerciseTotalDurationTrained(exerciseData.isCustom, exerciseData.id, hours, mins);

        // Display newly nmade workout and congratulate player.

        // for now this will just add the ammount of time trained to the exercises total time and save it,
        // as well as probably add that amoun to time to the players total time trained.

        // TODO: ONLY after everything is successfully done anc checked.
        workoutEntryPanel.SetUiInactive(true);
        workoutCataloguePanel.SetUiActive();
    }
}
