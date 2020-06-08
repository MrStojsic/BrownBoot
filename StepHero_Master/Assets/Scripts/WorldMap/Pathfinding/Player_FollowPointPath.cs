using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FollowPointPath : MonoBehaviour
{
    private float speed = 0.5f;
    private Vector2 goalPosition;
    private Vector2 position;
    public Vector2 possiblePoint;
    [Range(0, 1)]
    public float percent = 1f;

    public Vector2[] pointPath;

    private int currentPointIndex = 0;
    [SerializeField]
    private Vector2 nextPoint;

    [SerializeField]
    private float distanceRequired;
    [SerializeField]
    private float distancTraversable;

    public bool canMove = false;

   public void SetupPath(Vector2[] path)
   {
        if (path.Length > 0)
        {
            pointPath = path;
            nextPoint = pointPath[0];
            possiblePoint = nextPoint;
            canMove = true;

            // TODO : this is to remove the distance between the startnode and the first pointPath index
            //        until we impliment starting from last waypoint.
            distancTraversable -= Vector2.Distance(transform.position, nextPoint);
        }
    }

    void Update()
    {
        if (canMove == true)
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
        if (currentPointIndex + 1 < pointPath.Length)
        {

            distanceRequired = Vector2.Distance(nextPoint, pointPath[currentPointIndex+1]);
            
            if (distanceRequired > 0)
            {
                percent = distanceRequired <= distancTraversable ? 1 : distancTraversable / distanceRequired;
            }
            currentPointIndex++; // 0.
            nextPoint = pointPath[currentPointIndex];
            goalPosition = nextPoint;
            possiblePoint = Vector2.Lerp(position, goalPosition, percent);
        }
        else
        {
            //- This is when we would actually check to look for next node on the astar path and connect to the new path.
            canMove = false;
        }
    }
}
