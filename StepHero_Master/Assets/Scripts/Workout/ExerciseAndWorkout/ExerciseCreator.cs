using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ExerciseCreator : MonoBehaviour
{
    [SerializeField] ExerciseSelectionManager exerciseSelectionManager = null;
    [SerializeField] SelectorGroup iconSelectorGroup;
    [SerializeField] SetSpriteToExerciseIcon setSpriteToExerciseIcon;
    [SerializeField] SelectorGroup exerciseSelectorGroup;
    [SerializeField] TabGroup tabGroup;
    [SerializeField] SetActiveUiElement setActiveUiElement;

    public string _exerciseName = null;
    public string ExerciseName { get => _exerciseName; }


    public int _exerciseType = 0;
    public int ExerciseType { get => _exerciseType; }


    public int _iconIndex = 0;
    public int IconIndex { get => _iconIndex; }

    [SerializeField] private InputField exerciseNameInputField;

    [SerializeField] Color[] flashColours;

  

    public void ResetExerciseDetails(int exerciseType)
    {
        _exerciseName = null;
        exerciseNameInputField.text = null;
        exerciseNameInputField.Select();

        exerciseSelectorGroup.SelectSelectorViaIndex(exerciseType);

        _iconIndex = 0;
        setSpriteToExerciseIcon.SetSprite(_iconIndex);
    }

    // Start is called before the first frame update
    public void PassExerciseDetails()
    {
        _exerciseName = exerciseNameInputField.text;
        if (_exerciseName.Length > 0)
        {
            exerciseSelectionManager.CreateNewExerciseAndAddToSeclectionPage(_exerciseName, (int)_exerciseType, _iconIndex);
            setActiveUiElement.SetUiInactive(true);
            tabGroup.SelectSelectorViaIndex((int)_exerciseType);
        }
        else
        {
            StartCoroutine(FlashTextBox());
        }
    }

    // Update is called once per frame
    public void SetExerciseType(int exerciseType)
    {
        _exerciseType = exerciseType;
        setSpriteToExerciseIcon.SetColour(_exerciseType);
    }
    public void SetIconIndex()
    {
        _iconIndex = iconSelectorGroup.selectedIndex;
        setSpriteToExerciseIcon.SetSprite(_iconIndex);
    }

    private IEnumerator FlashTextBox()
    {
        exerciseNameInputField.image.color = flashColours[1];
        yield return new WaitForSeconds(0.075f);
        exerciseNameInputField.image.color = flashColours[0];
        yield return new WaitForSeconds(0.1f);
        exerciseNameInputField.image.color = flashColours[1];
        yield return new WaitForSeconds(0.075f);
        exerciseNameInputField.image.color = flashColours[0];
    }
}
