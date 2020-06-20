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

    public enum MovementState
    {
        AWAITING_INSTRUCTION,
        MOVING,
        REACHED_GOAL,
        PAUSED_ON_NODE_FOR_EVENT,
        PAUSED_ON_EDGE_FOR_EVENT,
        OUT_OF_STEPS
    }
    [SerializeField] private MovementState movementState;

    public AStarNode TEMP_startNode;

    [SerializeField] AStarNode currentNode;
    int currentEdgeIndex = 0;

    // TODO - This along with the percet along the path will need to be included in the player save data,
    //        and rember to save when the player exits the app!
    public AStarEdge lastAStarEdge;

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

        // TODO - This is temp for testing, it will be controlled when setting up movement properly later.
        movementState = MovementState.MOVING;
        totalDistanceTravelled = 0;
    }
    public void InitialisePathfinding(AStarNode goalNode)
    {
        // TODO - impliment to be used for pathfinging..
    }

    void Update()
    {
        if (edgesToGoal[currentEdgeIndex] != null && movementState == MovementState.MOVING)
        {
            step = speed * Time.deltaTime;

            distanceTravelled += step;
            totalDistanceTravelled += step;

            if (totalDistanceTravelled > availibleDistance)
            {
                lastAStarEdge = edgesToGoal[currentEdgeIndex];
                movementState = MovementState.OUT_OF_STEPS;
            }

            transform.position = edgesToGoal[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);

            if (distanceTravelled >= edgesToGoal[currentEdgeIndex].pathCreator.path.length)
            {
                NextEgde();
            }
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
            movementState = MovementState.MOVING;
        }
        else
        {
            lastAStarEdge = edgesToGoal[currentEdgeIndex];
            movementState = MovementState.REACHED_GOAL;
        }
    }
}
