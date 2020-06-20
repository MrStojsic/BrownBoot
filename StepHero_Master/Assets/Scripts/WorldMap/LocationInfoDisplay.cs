﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoDisplay : MonoBehaviour
{
    [SerializeField]Text title;
    AStarNode node;

    [SerializeField] Player_FollowBezierPath player_FollowBezierPath;

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
        title.text = location.TestString;
        node = location.GetComponent<AStarNode>();
    }
    public void SetPlayerGoalNode(AStarNode aStarNode)
    {
        player_FollowBezierPath.InitialisePathfinding(node);

    }
}