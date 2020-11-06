using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoDisplay : MonoBehaviour
{
    [SerializeField]Text title = default;
    AStarNode node = default;

    [SerializeField] Player_FollowBezierPath player_FollowBezierPath = default;

    [SerializeField] GameObject enterButton = null;
    [SerializeField] GameObject setOffButton = null;

    [SerializeField] private BreadCrumb_FollowBezierPath breadCrumb_FollowBezierPath = null;


    public void SetLocationDetails(Location location)
    {
        title.text = location.name; // NOTE: this .name is the Objects name in the hierachy.
        node = location.GetComponent<AStarNode>();

            // NOTE This just shows how to load an asset at runtime, this can be used to load a towns shops at runtime.
            LocationPoi tempName = (LocationPoi)Resources.Load("LocationData/" + location.name);
        if (tempName != null)
            tempName.TEST_PrintMerchantName(); 

        SetBreadCrumbGoalNode();
    }
    public void SetPlayerGoalNode()
    {
        // TODO.
        player_FollowBezierPath.InitialisePathfinding(node);

    }
    public void SetBreadCrumbGoalNode()
    {
        // TODO.
        breadCrumb_FollowBezierPath.InitialisePathfinding(node);

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
}
