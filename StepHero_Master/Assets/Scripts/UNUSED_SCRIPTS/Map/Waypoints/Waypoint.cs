using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;


    public float distanceToNextWaypoint;

    public Vector3 GetPosition()
    {
        return transform.position;
    }


    public float GetDistanceToNextWaypoint()
    {
        if (nextWaypoint != null)
        {
            distanceToNextWaypoint = Vector2.Distance(transform.position, nextWaypoint.GetPosition()) ;
            if (distanceToNextWaypoint <= 0)
            {
                Debug.LogError("Waypoint " + this.name + " & Waypoint " + nextWaypoint.name + " share a position, this is not allowed");
            }
        }
        else
        {
            distanceToNextWaypoint = 0;
        }
        return distanceToNextWaypoint;
    }

    public float BakeTotalPathDistance(float totalPathLenght = 0)
    {
        float total = totalPathLenght;
        GetDistanceToNextWaypoint();
        if (nextWaypoint != null)
        {
            totalPathLenght = nextWaypoint.BakeTotalPathDistance(GetDistanceToNextWaypoint() + totalPathLenght);
        }
        return totalPathLenght;
    }

}
