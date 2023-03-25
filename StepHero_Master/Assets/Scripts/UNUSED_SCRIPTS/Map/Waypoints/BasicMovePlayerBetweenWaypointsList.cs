using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovePlayerBetweenWaypointsList : MonoBehaviour
{
    private float speed = 1.5f;
    private Vector2 goalPosition;
    private Vector2 position;
    public Vector2 possiblePoint;
    [Range(0, 1)]
    public float percent = 1f;

    public Waypoint[] waypoints;
    public GameObject waypointRoot;
    private int currentWaypointIndex = 0;
    [SerializeField]
    private Waypoint goalWaypoint;

    [SerializeField]
    private float distanceRequired;
    [SerializeField]
    private float distancTraversable;

    void Start()
    {
        if (waypointRoot.transform.childCount > 0)
        {
            waypoints = waypointRoot.transform.GetComponentsInChildren<Waypoint>();
            goalWaypoint = waypoints[currentWaypointIndex];
            possiblePoint = goalWaypoint.GetPosition();


        }

    }

    void Update()
    {
        if (goalWaypoint != null)
        {
            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        position = transform.position;

        if (Vector2.Distance(position, possiblePoint) > 0f)
        {
            transform.position = Vector2.MoveTowards(position, possiblePoint, speed * Time.deltaTime);
        }
        else
        {
            NextWaypointInPath();
        }
    }

    private void NextWaypointInPath()
    {
        distancTraversable -= distanceRequired;
        if (percent < 1)
        {
            distancTraversable = 0;
            distanceRequired *= (1 - percent);
        }
        if (currentWaypointIndex + 1 < waypoints.Length)
        {
            if (goalWaypoint.nextWaypoint != null)
            {
                distanceRequired = Vector2.Distance(goalWaypoint.GetPosition(), goalWaypoint.nextWaypoint.GetPosition());
            }
            if (distanceRequired > 0)
            {
                percent = distanceRequired <= distancTraversable ? 1 : distancTraversable / distanceRequired;
            }
            currentWaypointIndex++; // 0.
            goalWaypoint = waypoints[currentWaypointIndex];
            goalPosition = goalWaypoint.transform.position;
            possiblePoint = Vector2.Lerp(position, goalPosition, percent);
        }
        else
        {
            //- This is when we would actually check to look for next node on the astar path and connect to the new path.
            goalWaypoint = null;
        }
    }

    /*
    // NO LONGER USING BUT KEEPING JUST INCASE "NextWaypointInPath" BREAKS.
    private void OldNextWaypointInPath()
    {
        if (currentWaypointIndex + 1 < waypoints.Length)
        {
            currentWaypointIndex++;
            goalWaypoint = waypoints[currentWaypointIndex];
            if (goalWaypoint.previousWaypoint != null)
            {
                distanceRequired = Vector2.Distance(goalWaypoint.GetPosition(), goalWaypoint.previousWaypoint.GetPosition());
            }
            if (distancTraversable < distanceRequired)
            {
                percent = distancTraversable / distanceRequired;
                distancTraversable = 0;
            }
            else
            {
                distancTraversable -= distanceRequired;
                {
                    if (distancTraversable < 0)
                    {
                        distancTraversable = 0;
                    }
                }
            }
            goalPosition = goalWaypoint.transform.position;
            possiblePoint = Vector2.Lerp(position, goalPosition, percent);
        }
        else
        {
            //- This is when we would actually check to look for next node on the astar path and connect to the new path.
            goalWaypoint = null;
        }
    }*/
}
