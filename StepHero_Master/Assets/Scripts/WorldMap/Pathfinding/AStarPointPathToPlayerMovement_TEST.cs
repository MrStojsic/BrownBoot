﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPointPathToPlayerMovement_TEST : MonoBehaviour
{
    public AStarPathfinder aStarPathfinder;
    public Player_FollowPointPath player_FollowPointPath;

    // Start is called before the first frame update
    public void BeginPathFollow()
    {
        if (aStarPathfinder.pointPath.Count > 0)
        {
            print("Run");
            player_FollowPointPath.SetupPath(aStarPathfinder.pointPath.ToArray());

        }
    }
}
