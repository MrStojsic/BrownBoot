using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer),typeof(SpriteButton))]
public class Location : MonoBehaviour
{
    [SerializeField] private MapUiManager mapUiManager;

    public Treasure treasure;

    public IInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {


        interactable = treasure;
    }

    // Update is called once per frame
    public void DisplayLocationInfo()
    {
        
        MapInteraction_Window tempMIW = UiWindowManager.GetUiPanel<MapInteraction_Window>();

        //print("Player is at current selected node : " + (Player_FollowBezierPath.instance.CurrentLocation == this));
        tempMIW.PresetDisplay(this, Player_FollowBezierPath.instance.CurrentLocation == this);

        UiWindowManager.Show(tempMIW);
    }
}
