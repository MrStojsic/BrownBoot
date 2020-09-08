using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetSpriteToExerciseIcon : MonoBehaviour
{
    [SerializeField] Image image = default;
    [SerializeField] SimpleButton simpleButton = default;
    // Start is called before the first frame update
    public void SetSprite(int spriteIndex)
    {
        image.sprite = ExerciseManager.instance.spriteArray[spriteIndex];
    }
    public void SetColour(int colourIndex)
    {
        simpleButton.buttonDefaultColour = ExerciseManager.instance.exerciseTypeColoursArray[colourIndex];
        simpleButton.buttonPressedColour = new Color( simpleButton.buttonDefaultColour.r * .9f, simpleButton.buttonDefaultColour.g * .9f, simpleButton.buttonDefaultColour.b * .9f,1) ;
        image.color = ExerciseManager.instance.exerciseTypeColoursArray[colourIndex];

    }
}
