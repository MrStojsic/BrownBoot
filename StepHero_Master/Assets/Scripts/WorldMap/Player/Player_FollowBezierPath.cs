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

    // PATHFINDING.
    [Header("Pathfinding")]
    public AStarPathfinder AStarPathfinder;
    public List<AStarEdge> edgesPath = new List<AStarEdge>();
    int currentEdgeIndex = 0;

    // MOVEMENT.
    [Header("Movement")]
    public float speed = 1.5f;
    private float step;

    // DISTANCE.
    [Header("Distance")]
    public float distanceTravelledOnEdge; // SAVEDATA
    public float totalDistanceTravelled = 0; // SAVEDATA
    public float availableDistance = 0; // SAVEDATA

    // NODES & PATHS.
    [Header("Nodes & Paths")]
    [SerializeField]
    AStarNode currentGoalNode;

    [SerializeField]
    private AStarEdge _currentAStarEdge; // SAVEDATA
    public AStarEdge CurrentAStarEdge
    {
        get { return _currentAStarEdge; }
    }
    [SerializeField]
    bool isForward = false;

    // STATES.
    [Header("States")]
    [SerializeField]
    private MovementState _currentMovementState;
    public MovementState CurrentMovementState
    {
        get { return _currentMovementState; }
    }
    public enum MovementState
    {
        AWAITING_INSTRUCTION,
        MOVING,
        REACHED_GOAL,
        PAUSED_ON_NODE_FOR_EVENT,
        PAUSED_ON_EDGE_FOR_EVENT,
        OUT_OF_STEPS
    }

    // ---------------------------

     private void Start()
     {
        // TODO > the following 1 line is only here until the players movement data can be saved.
        distanceTravelledOnEdge = _currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position); // TEMP.
        transform.position = _currentAStarEdge.pathCreator.path.GetPointAtDistance(distanceTravelledOnEdge, EndOfPathInstruction.Stop);
    }

     public void InitialisePathfinding(AStarNode destionationNode)
     {
        CancelJourney();

        AStarNode startNode = null;

        if (destionationNode == _currentAStarEdge.headNode || destionationNode == _currentAStarEdge.tailNode)
        {
            edgesPath.Insert(0, _currentAStarEdge);
            startNode = _currentAStarEdge.ReturnOtherEndOfPath(destionationNode);

        }
        else
        {
            // - Get the current edge, pick eith head of tail,
            //   if the current edge is index 0 of the returned list of paths we know we go the right starting node as we got a path to it and wont skip to it
            //   if the current edge is NOT index 0 of the returned list, we must have gotten the wrong end, in which case traverse the current path before staring to follow index 0.
            startNode = _currentAStarEdge.headNode;

            edgesPath = AStarPathfinder.FindPath(startNode, destionationNode);
            if (edgesPath[0] != _currentAStarEdge)
            {
                edgesPath.Insert(0, _currentAStarEdge);
                startNode = _currentAStarEdge.tailNode;
            }

            // TESTING
            float totalLength = 0;
            totalLength += edgesPath[0].LScore - distanceTravelledOnEdge;
            for (int i = 1; i < edgesPath.Count; i++)
            {
                totalLength += edgesPath[i].LScore;
            }
            print(totalLength);
            // TOHERE
        }

        currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(startNode);
        if (edgesPath != null)
        {
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            if (!isForward)
            {
                distanceTravelledOnEdge = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelledOnEdge;
            }
            ChangeMovementState(MovementState.MOVING);
        }
    }



    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_currentMovementState == MovementState.MOVING && edgesPath != null)
        {
            step = speed * Time.deltaTime;

            distanceTravelledOnEdge += step;
            totalDistanceTravelled += step;

            transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelledOnEdge, isForward, EndOfPathInstruction.Stop);

            if (totalDistanceTravelled > availableDistance)
            {
                WrapUpEndOfMovement();
                ChangeMovementState(MovementState.OUT_OF_STEPS);
                return;
            }


            if (distanceTravelledOnEdge >= edgesPath[currentEdgeIndex].pathCreator.path.length)
            {
                NextEgde();
            }

            /*
            // This is a more computational check for total distance but it seems like it will be more accurate over long journeys.
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
            */
        }
    }

    void NextEgde()
    {
        if (currentGoalNode.Location != null)
        {
            MapUiManager.Instance.DisplayMapEnterWindow(currentGoalNode.Location);


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
            distanceTravelledOnEdge = 0;
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
        _currentAStarEdge = edgesPath[currentEdgeIndex];
        if (!isForward)
        {
            distanceTravelledOnEdge = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelledOnEdge;
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

    public void ContinueJourney()
    {
        if (_currentMovementState != MovementState.REACHED_GOAL && _currentMovementState != MovementState.OUT_OF_STEPS)
        {
            ChangeMovementState(MovementState.MOVING);
        }
    }

    private void ChangeMovementState(MovementState movementState)
    {
        this._currentMovementState = movementState;
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
