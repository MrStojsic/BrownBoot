using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabExercise = null;

    [SerializeField] private SetActiveUiElement setActiveUiElement = null;

    // Start is called before the first frame update
    void Start()
    {
        AddDefaultExercisesToContentPages();
        AddLoadedExercisesToContentPages();
    }
    public void CreateNewExerciseAndAddToSeclectionPage(string _exerciseName, int _exerciseType, int _iconIndex)
    {
        // NOTE: Not setting the exerciseGo as a schild of this.transform gives it an incorrect scales on instantiation, we correct its parent later in the function.
        GameObject exerciseGo = Instantiate(prefabExercise, this.transform);
        ExerciseComponent exerciseComponent = exerciseGo.GetComponent<ExerciseComponent>();

        exerciseComponent.exerciseData = ExerciseManager.instance.CreateNewExercise(_exerciseName, _exerciseType, _iconIndex);
        exerciseComponent.iconSprite = ExerciseManager.instance.spriteArray[_iconIndex];

        exerciseGo.transform.SetParent(transform.GetChild(_exerciseType));
        // NOTE: Sibling index is set to this trans child based on the exercise type, and is set to the child count - 2, so its set above the create exercise item,
        //       and still in the correct order of FIFO.
        exerciseGo.transform.SetSiblingIndex(transform.GetChild(_exerciseType).childCount - 2);
        exerciseGo.name += exerciseComponent.exerciseData.exerciseName;
    }

    void AddDefaultExercisesToContentPages()
    {
        for (int i = 0; i < ExerciseManager.instance.defaultExerciseDataList.Count; i++)
        {
            InitialiseExerciseGameObject(i, true);
        }
    }

    public void AddLoadedExercisesToContentPages()
    {
        if (ExerciseManager.instance.customExerciseDataDictionary != null)
        {
            List<int> keys = new List<int>(ExerciseManager.instance.customExerciseDataDictionary.Keys);
            keys.Sort();
            foreach (int key in keys)
            {
                InitialiseExerciseGameObject(key, false);
            }
        }
    }
    private void InitialiseExerciseGameObject(int i, bool isDefault)
    {
        GameObject exerciseGo = Instantiate(prefabExercise, this.transform);

        ExerciseComponent exerciseComponent = exerciseGo.GetComponent<ExerciseComponent>();
        if (exerciseComponent != null)
        {
            if (isDefault)
            {
                exerciseComponent.exerciseData = ExerciseManager.instance.defaultExerciseDataList[i];
                exerciseGo.GetComponent<SwipeButton>().onSwipeLeft = null;
            }
            else
            {
                exerciseComponent.exerciseData = ExerciseManager.instance.customExerciseDataDictionary[i];
            }
            exerciseComponent.iconSprite = ExerciseManager.instance.spriteArray[exerciseComponent.exerciseData.iconIndex];
            exerciseGo.transform.SetParent(transform.GetChild((int)exerciseComponent.exerciseData.exerciseType));
            // NOTE: Sibling index is set to this trans child based on the exercise type, and is set to the child count - 2, so its set above the create exercise item,
            //       and still in the correct order of FIFO.
            exerciseGo.transform.SetSiblingIndex(transform.GetChild((int)exerciseComponent.exerciseData.exerciseType).childCount - 2);
            exerciseGo.name += exerciseComponent.exerciseData.exerciseName;
        }
        else
        {
            print("No Exercise component found on GameObject.");
        }
    }
}
