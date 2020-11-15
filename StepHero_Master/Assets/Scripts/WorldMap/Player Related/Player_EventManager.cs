using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Player_EventManager : MonoBehaviour
{

    public static Player_EventManager instance = null;

    private void Awake()
    {
        CreateInstance();
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
            Debug.LogError("Duplicate Player_EventManager CANNOT exist! " + this.name + " is a duplicate");
        } 
    }

    public InteractableEvent chosenEvent = null;

    public void InitialisePossibleEvent(AStarEdge aStarEdge)
    {
        if (aStarEdge.randomPathEvent)
        {
            chosenEvent = aStarEdge.randomPathEvent.RollEvent();
        }
    }

    public bool Evaluate(float distanceTraversed)
    {
        if (distanceTraversed >= chosenEvent.TriggerDistanceAlongPath)
        {
            TriggerEvent();
            return true;
        }
        return false;
    }

    public void TriggerEvent()
    {

        print("Event was triggered!!");
    }


}
