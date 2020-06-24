﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Player_FollowBezierPath : MonoBehaviour
{
    public List<AStarEdge> edgesPath = new List<AStarEdge>();

    public float speed = 5;
    public float distanceTravelled; // SAVEDATA

    [SerializeField] AStarNode currentGoalNode;

    int currentEdgeIndex = 0;
    [SerializeField] Vector3 lastWorldPosition;

    public AStarPathfinder AStarPathfinder;






    // TODO - This along with the percet along the path will need to be included in the player save data,
    //        and rember to save when the player exits the app!
    public AStarEdge currentAStarEdge; // SAVEDATA

    public float totalDistanceTravelled = 0; // SAVEDATA

    public float availableDistance = 0; // SAVEDATA
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
        distanceTravelled = currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        transform.position = currentAStarEdge.pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

    }


     public void InitialisePathfinding(AStarNode destionationNode)
     {
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
        //print(currentGoalNode.name);
        if (edgesPath != null)
        {
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            if (!isForward)
            {
                distanceTravelled = edgesPath[currentEdgeIndex].pathCreator.path.length - distanceTravelled;
            }
            else { distanceTravelled = 0; }
           // print(isForward);
            StartMoving();
        }

        print("InitialisePathfinding - currentAStarEdge " + currentAStarEdge.name + " - isForward " + isForward + " - currentEdgeIndex" + currentEdgeIndex + " - distanceTravelled  " + distanceTravelled + " - currentGoalNode " + currentGoalNode.name);
    }
    void StartMoving()
    {

        // TODO - This is temp for testing, it will be controlled when setting up movement properly later.
        movementState = MovementState.MOVING;

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
                currentAStarEdge = edgesPath[currentEdgeIndex];
                movementState = MovementState.OUT_OF_STEPS;
            }

            transform.position = edgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistanceByDirection(distanceTravelled, isForward, EndOfPathInstruction.Stop);

            if (distanceTravelled >= edgesPath[currentEdgeIndex].pathCreator.path.length)
            {
                movementState = MovementState.AWAITING_INSTRUCTION;
                NextEgde();
            }
        }
    }
    void NextEgde()
    {
        if (currentEdgeIndex + 1 < edgesPath.Count)
        {
            currentEdgeIndex++;

            currentGoalNode = edgesPath[currentEdgeIndex].ReturnOtherEndOfPath(currentGoalNode);
            isForward = edgesPath[currentEdgeIndex].headNode == currentGoalNode;
            distanceTravelled = 0;
            movementState = MovementState.MOVING;
        }
        else
        {
            currentAStarEdge = edgesPath[currentEdgeIndex];
            ResetEdgesPathList();
            movementState = MovementState.REACHED_GOAL;
            
        }
        print("NextEgde - currentAStarEdge " + currentAStarEdge.name + " - isForward " + isForward + " - currentEdgeIndex" + currentEdgeIndex + " - distanceTravelled  " + distanceTravelled + " - currentGoalNode " + currentGoalNode.name);
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
