using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Player_FollowBezierPath : MonoBehaviour
{
    public static Player_FollowBezierPath instance = null;

    private void Awake()
    {
        CreateInstance();
    }
    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Debug.LogError("Duplicate Player_FollowBezierPath CANNOT exist! " + this.name + " is a duplicate");
        }
    }

    public List<AStarEdge> edgesPath = new List<AStarEdge>();

    public float speed = 1.5f;
    public float distanceTravelled; // SAVEDATA

    [SerializeField] AStarNode currentGoalNode;

    int currentEdgeIndex = 0;

    public AStarPathfinder AStarPathfinder;


    public AStarEdge currentAStarEdge; // SAVEDATA

    public float totalDistanceTravelled = 0; // SAVEDATA

    public float availableDistance = 0; // SAVEDATA
    float step;
    [SerializeField] bool isForward = false;

    [SerializeField] private MapUiManager mapUiManager;

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
        // TODO > the following 1 line is only here until the players movement data can be saved.
        distanceTravelled = currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position); // TEMP.
        transform.position = currentAStarEdge.pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }

     public void InitialisePathfinding(AStarNode destionationNode)
     {
        CancelJourney();

        currentEdgeIndex = 0;
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

        }

        currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(startNode);
        if (edgesPath != null)
        {
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            if (!isForward)
            {
                distanceTravelled = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelled;
            }
            StartMoving();
        }

        //print("InitialisePathfinding - currentAStarEdge " + currentAStarEdge.name + " - isForward " + isForward + " - currentEdgeIndex" + currentEdgeIndex + " - distanceTravelled  " + distanceTravelled + " - currentGoalNode " + currentGoalNode.name);
    }
    private void StartMoving()
    {
        ChangeMovementState(MovementState.MOVING);
    }


    void Update()
    {
        if (movementState == MovementState.MOVING && edgesPath != null)
        {
            step = speed * Time.deltaTime;

            distanceTravelled += step;
            totalDistanceTravelled += step;

            if (totalDistanceTravelled > availableDistance)
            {
                WrapUpEndOfMovement();
                ChangeMovementState(MovementState.OUT_OF_STEPS);
            }

            transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);

 /*
                if (Player_EventManager.instance.Evaluate(isForward ? distanceTravelled : edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelled))
                {
                    ChangeMovementState(MovementState.PAUSED_ON_EDGE_FOR_EVENT);
                }
            */

            if (distanceTravelled >= edgesPath[currentEdgeIndex].pathCreator.path.length)
            {
                NextEgde();
            }
        }
    }
    void NextEgde()
    {
      
        if (currentGoalNode.Location != null)
        {
            mapUiManager.DisplayMapEnterWindow(currentGoalNode.Location);
            ChangeMovementState(MovementState.PAUSED_ON_NODE_FOR_EVENT);

            // DEBUG TESTING ONLY......
            currentGoalNode.Location.interactable?.Interact();
            // .......
        }



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
        //print("NextEgde - currentAStarEdge " + currentAStarEdge.name + " - isForward " + isForward + " - currentEdgeIndex" + currentEdgeIndex + " - distanceTravelled  " + distanceTravelled + " - currentGoalNode " + currentGoalNode.name);
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

    public void ChangeMovementState(MovementState movementState)
    {
        this.movementState = movementState;
    }


    private void ResetEdgesPathList()
    {
        edgesPath.Clear();
        currentEdgeIndex = 0;
    }

    public void AddStepsToAvailableDistance(int steps)
    {
        availableDistance += (steps * .75f);
    }
}
