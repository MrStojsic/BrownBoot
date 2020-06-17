using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Player_FollowBezierPath : MonoBehaviour
{
    public List<AStarEdge> edgesToGoal = new List<AStarEdge>();

    public float speed = 5;
    public float distanceTravelled;
    public bool reachedNode = false;

    public AStarNode TEMP_startNode;
    [SerializeField] AStarNode currentNode;
    int currentEdgeIndex = 0;

    public float totalDistanceTravelled = 0;

    public float availibleDistance = 0;
    float step;
    [SerializeField] bool isForward = false;

    void Start()
    {
        InitialisePath(TEMP_startNode);
    }
    void InitialisePath(AStarNode starNode)
    {
        isForward = edgesToGoal[currentEdgeIndex].headNode == starNode;
        //- Set distanceTravelled to the closest point on the path to the players starting posistion.
       
        distanceTravelled = isForward ? edgesToGoal[0].pathCreator.path.GetClosestDistanceAlongPath(this.transform.position): edgesToGoal[0].pathCreator.path.length - edgesToGoal[0].pathCreator.path.GetClosestDistanceAlongPath(this.transform.position);
       

        print("TEST_"+edgesToGoal[currentEdgeIndex].pathCreator.path.GetClosestTimeOnPath(transform.position));
        print("distance "+edgesToGoal[currentEdgeIndex].pathCreator.path.GetClosestDistanceAlongPath(this.transform.position));
        print("lenght "+edgesToGoal[currentEdgeIndex].pathCreator.path.length);
    }

    void Update()
    {
        if (edgesToGoal[currentEdgeIndex] != null && reachedNode == false)
        {
            
            step = speed * Time.deltaTime;

            distanceTravelled += step;
            totalDistanceTravelled += step;
     

            transform.position = edgesToGoal[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);
           
            if (distanceTravelled >= edgesToGoal[currentEdgeIndex].pathCreator.path.length)
            {
                reachedNode = true;
                NextEgde();


            }
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
    void NextEgde()
    {
        if (currentEdgeIndex + 1 < edgesToGoal.Count)
        {
            currentEdgeIndex++;

            currentNode = edgesToGoal[currentEdgeIndex].ReturnOtherEndOfPath(currentNode);
            isForward = edgesToGoal[currentEdgeIndex].headNode == currentNode;
            distanceTravelled = isForward ? 0 : edgesToGoal[currentEdgeIndex].pathCreator.path.length;
            reachedNode = false;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        // NOT CURRENTLY USED - path doesnt change during play.
        distanceTravelled = edgesToGoal[0].pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
