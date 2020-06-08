using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutCatalogueManager : MonoBehaviour
{
    [SerializeField] GameObject prefabWorkout;

    [SerializeField] List<WorkoutComponent> workoutComponentsList = new List<WorkoutComponent>();


    // Start is called before the first frame update
    void Start()
    {
        FillWorkoutContentPages();

    }
    public void EnterNewWorkoutAndAddToCalaloguePage(bool _isCustom, int _id, int _hours, int _mins)
    {
        WorkoutManager.instance.CreateNewWorkout(_isCustom, _id, _hours, _mins);
        FillWorkoutContentPages();
    }

    void FillWorkoutContentPages()
    {
        for (int i = 0; i < WorkoutManager.instance.workoutList.Count; i++)
        {

            if (workoutComponentsList.Count > 0 && workoutComponentsList.Count > i)
            {
               
                workoutComponentsList[i].workoutData = WorkoutManager.instance.workoutList[i];
                workoutComponentsList[i].SetUi();
                workoutComponentsList[i].gameObject.SetActive(true);
            }
            else
            {
                GameObject workoutGo = Instantiate(prefabWorkout, transform);
                WorkoutComponent workoutComponent = workoutGo.GetComponent<WorkoutComponent>();
                workoutComponentsList.Add(workoutComponent);
                workoutComponent.workoutData = WorkoutManager.instance.workoutList[i];
            }
        }





        /*
        for (int i = 0; i < WorkoutManager.instance.workoutList.Count; i++)
        {
            GameObject workoutGo = Instantiate(prefabWorkout, this.transform);

            WorkoutComponent workoutComponent = workoutGo.GetComponent<WorkoutComponent>();
            if (workoutComponent != null)
            {
                workoutComponent.workoutData = WorkoutManager.instance.workoutList[i];

                workoutGo.name += workoutComponent.workoutData.id;
            }
            else
            {
                print("No Workout component found on GameObject.");
            }
        }*/
    }
    public void DisableChildAtIndex(int childIndex)
    {
        transform.GetChild(childIndex).gameObject.SetActive(false);
    }
}
