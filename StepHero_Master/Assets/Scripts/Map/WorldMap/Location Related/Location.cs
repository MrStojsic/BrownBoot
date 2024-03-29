﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer),typeof(SpriteButton))]
public class Location : MonoBehaviour
{
    private MapUiManager mapUiManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // This is used to add a reference to this script in the objects AStar node when this component is added to an object.
    // NOTE: Reset is called when adding a component to an object.
    private void Reset()
    {
        GetComponent<AStarNode>().Location = this;
    }

    // Update is called once per frame
    public void DisplayLocationInfo()
    {
        
        MapInteraction_Window tempMIW = UiWindowManager.GetUiPanel<MapInteraction_Window>();

        bool isAtLocation = false;
        if (Player_FollowBezierPath.instance.CurrentLocation == this)
        {
            Player_FollowBezierPath.instance.CancelJourney();
            isAtLocation = true;
        }
        //print("Player is at current selected node : " + (Player_FollowBezierPath.instance.CurrentLocation == this));
        tempMIW.PresetDisplay(this, isAtLocation);

        // ALTERNATIVE - If the map isnt going to be interactable while the map interactiosn window is open the above 'IF' statment can all be replaces by this line.
        //tempMIW.PresetDisplay(this, Player_FollowBezierPath.instance.CurrentLocation == this);

        UiWindowManager.Show(tempMIW);
    } 
}
