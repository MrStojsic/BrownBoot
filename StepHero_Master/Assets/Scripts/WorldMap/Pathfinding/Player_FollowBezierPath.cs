using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Player_FollowBezierPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float distanceTravelled;
    public bool reachedEnd = false;

    void Start()
    {

    }

    void Update()
    {
        if (pathCreator != null && reachedEnd == false)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, false, endOfPathInstruction);

            if (distanceTravelled >= pathCreator.path.length && endOfPathInstruction != EndOfPathInstruction.Reverse)
            {
                reachedEnd = true;
                distanceTravelled = pathCreator.path.length;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            }
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
