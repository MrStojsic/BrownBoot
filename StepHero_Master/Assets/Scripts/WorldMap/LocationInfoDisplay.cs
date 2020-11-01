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

    public void SetLocationDetails(Location location)
    {
        title.text = location.name;
        node = location.GetComponent<AStarNode>();
    }
    public void SetPlayerGoalNode()
    {
        // TODO.
        player_FollowBezierPath.InitialisePathfinding(node);

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
