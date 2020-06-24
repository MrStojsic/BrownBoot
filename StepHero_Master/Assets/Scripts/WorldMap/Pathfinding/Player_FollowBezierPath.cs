using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Player_FollowBezierPath : MonoBehaviour
{
    public List<AStarEdge> edgesPath = new List<AStarEdge>();

    public float speed = 5;
    public float distanceTravelled; // SAVEDATA

    [SerializeField] AStarNode currentNode;
    AStarNode destinationNode;
    int currentEdgeIndex = 0;
    [SerializeField] Vector3 lastWorldPosition;

    public AStarPathfinder AStarPathfinder;






    // TODO - This along with the percet along the path will need to be included in the player save data,
    //        and rember to save when the player exits the app!
    public AStarEdge lastAStarEdge; // SAVEDATA

    public float totalDistanceTravelled = 0; // SAVEDATA

    public float availibleDistance = 0; // SAVEDATA
    float step;
    [SerializeField] bool isForward = false;

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

     private void Start()
     {
         transform.position = lastWorldPosition;
     }


     public void InitialisePathfinding(AStarNode destionationNode)
     {
         currentEdgeIndex = 0;
         this.destinationNode = destionationNode;

         AStarNode startNode = Vector3.Distance(lastAStarEdge.headNode.transform.position, destionationNode.transform.position)
                             > Vector3.Distance(lastAStarEdge.tailNode.transform.position, destionationNode.transform.position)
                             ? lastAStarEdge.headNode : lastAStarEdge.tailNode;
         edgesPath = AStarPathfinder.FindPath(startNode, destionationNode);
         if (edgesPath != null)
         {
             isForward = edgesPath[currentEdgeIndex].tailNode == startNode;
             StartMoving();
         }
     }
    void StartMoving()
    {
        print("TEST_"+edgesPath[currentEdgeIndex].pathCreator.path.GetClosestTimeOnPath(transform.position));
        print("distance "+edgesPath[currentEdgeIndex].pathCreator.path.GetClosestDistanceAlongPath(this.transform.position));
        print("lenght "+edgesPath[currentEdgeIndex].pathCreator.path.length);

        // TODO - This is temp for testing, it will be controlled when setting up movement properly later.
        movementState = MovementState.MOVING;
        totalDistanceTravelled = 0;
    }


    void Update()
    {
        if (movementState == MovementState.MOVING && edgesPath != null)
        {
            step = speed * Time.deltaTime;

            distanceTravelled += step;
            totalDistanceTravelled += step;

            if (totalDistanceTravelled > availibleDistance)
            {
                lastAStarEdge = edgesPath[currentEdgeIndex];
                movementState = MovementState.OUT_OF_STEPS;
            }

            transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);

            if (distanceTravelled >= edgesPath[currentEdgeIndex].pathCreator.path.length)
            {
                NextEgde();
            }
        }
    }
    void NextEgde()
    {
        if (currentEdgeIndex + 1 < edgesPath.Count)
        {
            currentEdgeIndex++;

            currentNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(currentNode);
            isForward = edgesPath[currentEdgeIndex].headNode == currentNode;
            distanceTravelled = isForward ? 0 : edgesPath[currentEdgeIndex].pathCreator.path.length;
            movementState = MovementState.MOVING;
        }
        else
        {
            lastAStarEdge = edgesPath[currentEdgeIndex];
            movementState = MovementState.REACHED_GOAL;
        }
    }
}
