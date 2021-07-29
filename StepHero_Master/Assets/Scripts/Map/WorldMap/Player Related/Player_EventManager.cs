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

    void printNum(int num)
    { print(num); }
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

    /*
    public IInteractable interactableEvent = null;

    public void InitialisePossibleEvent(AStarEdge aStarEdge)
    {
        if (aStarEdge.worldMapEventRoller)
        {
            interactableEvent = aStarEdge.worldMapEventRoller.RollEvent();
        }
    }
    */

    // function to check for an event when we reach a node
    // and a function to check for an event when we start on a new path.

    public void AttemptRollNodeEvent(AStarNode aStarNode)
    {
        if (aStarNode.WorldMapNodeEncounter)
        {
            print(aStarNode.WorldMapNodeEncounter.RollEvent());
        }
    }

    public void TriggerEvent()
    {

        print("Event was triggered!!");
    }


}
