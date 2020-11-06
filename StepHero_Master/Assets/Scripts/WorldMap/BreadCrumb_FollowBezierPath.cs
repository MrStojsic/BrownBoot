using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BreadCrumb_FollowBezierPath : MonoBehaviour
{

    public List<AStarEdge> edgesPath = new List<AStarEdge>();

    public float speed = 1.5f;
    public float distanceTravelled; // SAVEDATA

    [SerializeField] AStarNode currentGoalNode;

    int currentEdgeIndex = 0;

    public AStarPathfinder AStarPathfinder;


    public AStarEdge currentAStarEdge; // SAVEDATA

    public float totalDistanceTravelled = 0; // SAVEDATA

    float step;
    [SerializeField] bool isForward = false;

    public enum MovementState
    {
        AWAITING_INSTRUCTION,
        MOVING,
        REACHED_GOAL,
    }
    [SerializeField] private MovementState movementState;

    [SerializeField] private Player_FollowBezierPath player_FollowBezierPath;

    float startingDistanceTravelled = 0;

    private void Start()
    {
        ResetCrumb();
    }

    private void ResetCrumb()
    {
        currentAStarEdge = player_FollowBezierPath.CurrentAStarEdge;
        // TODO > the following 1 line is only here until the players movement data can be saved.
        distanceTravelled = player_FollowBezierPath.distanceTravelledOnEdge;   //currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position); // TEMP.
        startingDistanceTravelled = distanceTravelled;
        transform.position = currentAStarEdge.pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

    }

    public void InitialisePathfinding(AStarNode destionationNode)
     {
        CancelJourney();

        ResetCrumb();

        currentEdgeIndex = 0;
        totalDistanceTravelled = 0;
        AStarNode startNode = null;

        if (destionationNode == currentAStarEdge.headNode || destionationNode == currentAStarEdge.tailNode)
        {
            edgesPath.Insert(0, currentAStarEdge);
            startNode = currentAStarEdge.ReturnOtherEndOfPath(destionationNode);

        }
        else
        {
            // - Get the current edge, pick eith head of tail,
            //   if the current path is index 0 of the returned list of paths we know we go the right starting node as we got a path to it and wont skip to it
            //   if the current path is index 0 of the returned list, we must have gotten the wrong end, in which case traverse the current path before staring to follow index 0.
            startNode = currentAStarEdge.headNode;

            edgesPath = AStarPathfinder.FindPath(startNode, destionationNode);
            if (edgesPath[0] != currentAStarEdge)
            {
                edgesPath.Insert(0, currentAStarEdge);
                startNode = currentAStarEdge.tailNode;
            }

            // TESTING
            float totalLength = 0;
            totalLength += edgesPath[0].LScore - distanceTravelled;
            for (int i = 1; i < edgesPath.Count; i++)
            {
                totalLength += edgesPath[i].LScore;
            }
            //print(totalLength);
            // TOHERE
        }

        currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(startNode);
        if (edgesPath != null)
        {
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            if (!isForward)
            {
                distanceTravelled = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelled;
            }
            ChangeMovementState(MovementState.MOVING);
        }
  

        //print("InitialisePathfinding - currentAStarEdge " + currentAStarEdge.name + " - isForward " + isForward + " - currentEdgeIndex" + currentEdgeIndex + " - distanceTravelled  " + distanceTravelled + " - currentGoalNode " + currentGoalNode.name);
    }


    void Update()
    {

        // TODO: the distanceTravelled is currently based on the multiple of speed, we need to calculate the distance first.
        //       then set the position as a multiple of that distance so we get accurate disat ne travelled.
        if (movementState == MovementState.MOVING && edgesPath != null)
        {
            step = speed * Time.deltaTime;

            distanceTravelled += step;

            transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);

            if (distanceTravelled >= edgesPath[currentEdgeIndex].pathCreator.path.length)
            {
                distanceTravelled = edgesPath[currentEdgeIndex].pathCreator.path.length;
                if (currentEdgeIndex == 0)
                {

                    totalDistanceTravelled += edgesPath[currentEdgeIndex].pathCreator.path.length - startingDistanceTravelled;
                }
                else
                {

                    totalDistanceTravelled += distanceTravelled;
                }
                NextEgde();
            }
        }
    }

    void NextEgde()
    {

        if (currentEdgeIndex + 1 < edgesPath.Count)
        {
            currentEdgeIndex++;

            //Player_EventManager.instance.InitialisePossibleEvent(edgesPath[currentEdgeIndex]);

            currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(currentGoalNode);
            
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            distanceTravelled = 0;
        }
        else
        {
            WrapUpEndOfMovement();
            ChangeMovementState(MovementState.REACHED_GOAL);
        }
     }

    private void WrapUpEndOfMovement()
    {
        currentAStarEdge = edgesPath[currentEdgeIndex];
        if (!isForward)
        {
            distanceTravelled = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelled;
        }
        ResetEdgesPathList();
    }

    public void CancelJourney()
    {
        if (edgesPath.Count > 0)
        {
            WrapUpEndOfMovement();
            ChangeMovementState(MovementState.AWAITING_INSTRUCTION);
        }
    }


    private void ChangeMovementState(MovementState movementState)
    {
        this.movementState = movementState;
    }


    private void ResetEdgesPathList()
    {
        edgesPath.Clear();
        currentEdgeIndex = 0;
    }
}
