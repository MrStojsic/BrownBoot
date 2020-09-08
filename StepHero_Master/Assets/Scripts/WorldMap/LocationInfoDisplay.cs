using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoDisplay : MonoBehaviour
{
    [SerializeField]Text title = default;
    AStarNode node = default;

    [SerializeField] Player_FollowBezierPath player_FollowBezierPath = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
}
