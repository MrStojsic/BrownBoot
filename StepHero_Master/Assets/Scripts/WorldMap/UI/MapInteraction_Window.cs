using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]
public class MapInteraction_Window : UiWindow
{
    [SerializeField] Text title = default;
    [SerializeField] private Text journeyDistance = null;

    [SerializeField] private GameObject enterButton = null;
    [SerializeField] private GameObject setOffButton = null;


    [SerializeField] private BreadCrumb_FollowBezierPath breadCrumb_FollowBezierPath = null;

    public override void Initialise()
    {

    }

    public void ShowLocationDetails(Location location, bool canEnter)
    {
        title.text = location.name; // NOTE: this .name is the Objects name in the hierachy.
        AStarNode node = location.GetComponent<AStarNode>();

        // NOTE This just shows how to load an asset at runtime, this can be used to load a towns shops at runtime.
        LocationPoi tempName = (LocationPoi)Resources.Load("LocationData/" + location.name);
        if (tempName != null)
            tempName.TEST_PrintMerchantName();

        // TODO- handle which elements to display on the Map Interation Window.
        // For now we will use this crap.
        ToggleButton(canEnter);

        if (canEnter)
        {
            // Hide journeyDistance text.
            journeyDistance.text = "";


        }
        else
        {
            // Calculate journeyDistance and display it.
            float distanceToLocation = Player_FollowBezierPath.instance.CalculatePath(node);
            journeyDistance.text = distanceToLocation.ToString();

        }
    }

    public void InitiatePlayerBeginJourney()
    {
        UiWindowManager.ShowLast();
        Player_FollowBezierPath.instance.BeginJourney();
    }

    public void ToggleButton(bool isEnter)
    {
        if (isEnter)
        {
            setOffButton.SetActive(false);
            enterButton.SetActive(true);
        }
        else
        {
            enterButton.SetActive(false);
            setOffButton.SetActive(true);
        }
    }

    public void JourneyOn()
    {
        UiWindowManager.ShowLast();
        Player_FollowBezierPath.instance.ContinueJourney();
    }


    public void CancelJourney()
    {
        UiWindowManager.ShowLast();
        Player_FollowBezierPath.instance.CancelJourney();
    }


}
