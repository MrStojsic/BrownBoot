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
    public float currentDistanceTravelledOnEdge; // SAVEDATA
    public float totalDistanceTravelled = 0; // SAVEDATA
    public float availableDistance = 0; // SAVEDATA

    // NODES & PATHS.
    [Header("Nodes & Paths")]
    [SerializeField]
    AStarNode currentGoalNode;
        public Location lastLocation;

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

    // TEMP VARIABLES.
    [Range(0, 1)]
    public float percentageOfPathTraversed = .5f;

    // ---------------------------

    private void Start()
    {
        // TODO > the following 1 line is only here until the players movement data can be saved.
        //currentDistanceTravelledOnEdge = _currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position); // TEMP.
        currentDistanceTravelledOnEdge = _currentAStarEdge.pathCreator.path.length * percentageOfPathTraversed;// TEMP.
        isForward = true;

        transform.position = _currentAStarEdge.pathCreator.path.GetPointAtDistance(currentDistanceTravelledOnEdge, EndOfPathInstruction.Stop);
    }

    public float CalculatePath(AStarNode destionationNode)
    {


        // HACK - This wll probably be changed once LocationInfoDisplay is rewritten (it will check the state before calling CalculatePath). 
        if (_currentMovementState == MovementState.PAUSED_ON_NODE_FOR_EVENT)
        {
            return 0;
        }
        // TOHERE

        CancelJourney();

        AStarNode startNode = null;

        if (destionationNode == _currentAStarEdge.headNode || destionationNode == _currentAStarEdge.tailNode)
        {
            edgesPath.Insert(0, _currentAStarEdge);
            startNode = _currentAStarEdge.ReturnOtherEndOfPath(destionationNode);
        }
        else
        {
            // TODO - the problem seems to be when we travel a single node dist from one edge to another, so its when the node is one noe away but on a differnt edge.


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

        }
        currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(startNode);



        isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;

        float totalLength = 0;
        totalLength += isForward ? edgesPath[currentEdgeIndex].LScore - currentDistanceTravelledOnEdge : currentDistanceTravelledOnEdge;
        for (int i = 1; i < edgesPath.Count; i++)
        {
            totalLength += edgesPath[i].LScore;
        }
        print("Total Path Lenght = " + totalLength);

        return totalLength;
    }

    public void BeginJourney()
    {
        //currentDistanceTravelledOnEdge = isForward ? currentDistanceTravelledOnEdge : edgesPath[0].LScore - currentDistanceTravelledOnEdge;
        ChangeMovementState(MovementState.MOVING);
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

            if (isForward)
            {
                if (currentDistanceTravelledOnEdge + step > edgesPath[currentEdgeIndex].LScore)
                {
                    step = edgesPath[currentEdgeIndex].LScore - currentDistanceTravelledOnEdge;
                }

                currentDistanceTravelledOnEdge += step;

                totalDistanceTravelled += step;

                if (MoveForward() == false)
                    return;

                if (currentDistanceTravelledOnEdge >= edgesPath[currentEdgeIndex].pathCreator.path.length)
                {
                    NextEgde();
                }
            }
            else
            {
                if (currentDistanceTravelledOnEdge - step < 0)
                {
                    step = currentDistanceTravelledOnEdge;
                }

                currentDistanceTravelledOnEdge -= step;

                totalDistanceTravelled += step;

                if (MoveForward() == false)
                    return;

                if (currentDistanceTravelledOnEdge <= 0)
                {
                    NextEgde();
                }
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

    bool MoveForward()
    {
        transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistance(currentDistanceTravelledOnEdge, EndOfPathInstruction.Stop);

        if (totalDistanceTravelled > availableDistance)
        {
            WrapUpEndOfMovement(MovementState.OUT_OF_STEPS);
            return false;
        }
        return true;
    }

    void NextEgde()
    {
        if (currentGoalNode.Location != null && currentGoalNode.Location != lastLocation)
        {
            ChangeMovementState(MovementState.PAUSED_ON_NODE_FOR_EVENT);

            MapUiManager.Instance.DisplayMapEnterWindow(currentGoalNode.Location);

            lastLocation = currentGoalNode.Location;



            // DEBUG TESTING ONLY......
            currentGoalNode.Location.interactable?.Interact();
            // .......
        }

        if (currentEdgeIndex + 1 < edgesPath.Count)
        {
            currentEdgeIndex++;


            _currentAStarEdge = edgesPath[currentEdgeIndex];
            //Player_EventManager.instance.InitialisePossibleEvent(edgesPath[currentEdgeIndex]);

            currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(currentGoalNode);


            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            currentDistanceTravelledOnEdge = isForward ? 0 : edgesPath[currentEdgeIndex].LScore;
        }
        else
        {
            WrapUpEndOfMovement(MovementState.REACHED_GOAL);
        }
    }

    private void WrapUpEndOfMovement(MovementState endingMovementState)
    {
        if (edgesPath.Count > 0)
        {

            // This check must be performed before changing the movmentState.
            ResetEdgesPathList();
        }
        ChangeMovementState(endingMovementState);
    }

    public void CancelJourney()
    {
        WrapUpEndOfMovement(MovementState.AWAITING_INSTRUCTION);
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
