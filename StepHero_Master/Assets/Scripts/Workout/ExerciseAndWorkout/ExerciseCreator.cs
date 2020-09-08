using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ExerciseCreator : MonoBehaviour
{
    [SerializeField] ExerciseSelectionManager exerciseSelectionManager = default;
    [SerializeField] SelectorGroup iconSelectorGroup = default;
    [SerializeField] SetSpriteToExerciseIcon setSpriteToExerciseIcon = default;
    [SerializeField] SelectorGroup exerciseSelectorGroup = default;
    [SerializeField] TabGroup tabGroup = default;
    [SerializeField] SetActiveUiElement setActiveUiElement = default;

    public string _exerciseName = null;
    public string ExerciseName { get => _exerciseName; }


    public int _exerciseType = 0;
    public int ExerciseType { get => _exerciseType; }


    public int _iconIndex = 0;
    public int IconIndex { get => _iconIndex; }

    [SerializeField] private InputField exerciseNameInputField = default;

    [SerializeField] Color[] flashColours = default;

  

    public void ResetExerciseDetails(int exerciseType)
    {
        _exerciseName = null;
        exerciseNameInputField.text = null;
        exerciseNameInputField.Select();

        exerciseSelectorGroup.SelectSelectorViaIndex(exerciseType);


        _iconIndex = 0;
        setSpriteToExerciseIcon.SetSprite(_iconIndex);
        iconSelectorGroup.SelectSelectorViaIndex(0);
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
