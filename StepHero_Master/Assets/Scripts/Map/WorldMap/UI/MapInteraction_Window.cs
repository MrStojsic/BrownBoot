using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
public class MapInteraction_Window : UiWindow
{
    [SerializeField] Text title = default;
    [SerializeField] private Text journeyDistance = null;

    [SerializeField] private GameObject setOffButton = null;
    [SerializeField] private GameObject enterButton = null;
    [SerializeField] private GameObject journeyOnButton = null;
    [SerializeField] private GameObject closeButton = null;

    [SerializeField] private GameObject largeCancelJourneyButton = null;


    [SerializeField] private BreadCrumb_FollowBezierPath breadCrumb_FollowBezierPath = null;

    [SerializeField] private MapInteraction_LocationPoiPopulator locationPoiPopulator = null;


    public void PresetDisplay(Location location, bool isAtLocation)
    {
        title.text = location.name; // NOTE: this .name is the Objects name in the hierachy.


        locationPoiPopulator.PopulateLocationPoiList(location.name);


        if (isAtLocation)
        {
            // Hide journeyDistance text.
            journeyDistance.text = "";

            //TODO
            //  - When arriving at destination hide distance, display details, hide set as goal button, offer enter, hide journey on.
            if (Player_FollowBezierPath.instance.JourneyEndedAtLocation)
            {
                largeCancelJourneyButton.SetActive(false);
                setOffButton.SetActive(false);
                enterButton.SetActive(true);
                journeyOnButton.SetActive(false);
                closeButton.SetActive(true);

            }
            //TODO
            //  - When passing though hide distance, display details, hide set as goal button, offer enter and journey on.
            else
            {
                setOffButton.SetActive(false);
                enterButton.SetActive(true);
                journeyOnButton.SetActive(true);
                closeButton.SetActive(false);
            }
        }
        //TODO
        //  - Just show details, distance away and offer to set as goal, hide enter and journey on.
        else
        {
            // Calculate journeyDistance and display it.
            float distanceToLocation = Player_FollowBezierPath.instance.CalculatePath(location.GetComponent<AStarNode>());
    
            
                journeyDistance.text = distanceToLocation.ToString();

                setOffButton.SetActive(true);
                enterButton.SetActive(false);
                journeyOnButton.SetActive(false);
                closeButton.SetActive(true);
            
        }
    }

    public void InitiatePlayerBeginJourney()
    {
        if (gameObject.activeSelf == true)
        {
            UiWindowManager.ShowLast();
        }
        largeCancelJourneyButton.SetActive(true);
        Player_FollowBezierPath.instance.BeginJourney();
    }

    public void JourneyOn()
    {
        if (gameObject.activeSelf == true)
        {
            UiWindowManager.ShowLast();
        }
        Player_FollowBezierPath.instance.ContinueJourney();
    }


    public void CancelJourney()
    {
        if (gameObject.activeSelf == true)
        {
            UiWindowManager.ShowLast();
        }
        largeCancelJourneyButton.SetActive(false);
        Player_FollowBezierPath.instance.CancelJourney();
    }
}
