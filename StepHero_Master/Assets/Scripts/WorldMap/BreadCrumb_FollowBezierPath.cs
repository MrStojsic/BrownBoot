using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BreadCrumb_FollowBezierPath : MonoBehaviour
{

    private List<AStarEdge> playersEdgesPath = new List<AStarEdge>();

    [SerializeField] private float speed = 1.5f;
    private float currentDistanceTravelledOnEdge; // SAVEDATA

    private AStarNode currentGoalNode;

    private int currentEdgeIndex = 0;

    private float delayTimer = 0;
    [SerializeField] private float delayBetweenRuns = 2f;


    private AStarEdge currentAStarEdge; // SAVEDATA



    private float step;
    private bool isForward = false;

    public enum MovementState
    {
        AWAITING_INSTRUCTION,
        MOVING,
        REACHED_GOAL,
    }
    [SerializeField] private MovementState currentMovementState;

    [SerializeField] private Player_FollowBezierPath player_FollowBezierPath;

    [SerializeField] private TrailRenderer trailRenderer = null;
    private float startingTrailRendererTime = 0;


    private void Start()
    {
        startingTrailRendererTime = trailRenderer.time;
        ResetCrumb();

    }

    private void ResetCrumb()
    {
        delayTimer = 0;
        transform.position = Player_FollowBezierPath.instance.transform.position;
        trailRenderer.Clear();
        trailRenderer.time = startingTrailRendererTime;

        //currentAStarEdge = player_FollowBezierPath.CurrentAStarEdge;
        // TODO > the following 1 line is only here until the players movement data can be saved.
        // distanceTravelled = player_FollowBezierPath.currentDistanceTravelledOnEdge;   //currentAStarEdge.pathCreator.path.GetClosestDistanceAlongPath(transform.position); // TEMP.
        //startingDistanceTravelled = distanceTravelled;
        // transform.position = currentAStarEdge.pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

    }

    public void InitialiseBreadCrumb(AStarNode currentGoalNode, int currentEdgeIndex, bool isForward, float distanceTravelled)
    {


        this.currentEdgeIndex = currentEdgeIndex;

        playersEdgesPath = Player_FollowBezierPath.instance.edgesPath;

        this.currentGoalNode = currentGoalNode;

        this.isForward = isForward;

        this.currentDistanceTravelledOnEdge = distanceTravelled;

        ChangeMovementState(MovementState.MOVING);
        ResetCrumb();


    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (playersEdgesPath == null)
        {
            CancelJourney();
            return;
        }
        if (currentMovementState == MovementState.MOVING)
        {
            if (delayTimer < delayBetweenRuns)
            {
                delayTimer += Time.deltaTime;
            }

            step = speed * Time.deltaTime;

            if (isForward)
            {
                if (currentDistanceTravelledOnEdge + step > playersEdgesPath[currentEdgeIndex].LScore)
                {
                    step = playersEdgesPath[currentEdgeIndex].LScore - currentDistanceTravelledOnEdge;
                }

                currentDistanceTravelledOnEdge += step;


                transform.position = playersEdgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistance(currentDistanceTravelledOnEdge, EndOfPathInstruction.Stop);

                if (currentDistanceTravelledOnEdge >= playersEdgesPath[currentEdgeIndex].pathCreator.path.length)
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


                transform.position = playersEdgesPath[currentEdgeIndex].pathCreator.path.GetPointAtDistance(currentDistanceTravelledOnEdge, EndOfPathInstruction.Stop);

                if (currentDistanceTravelledOnEdge <= 0)
                {
                    NextEgde();
                }
            }
        }
    }

    void NextEgde()
    {
        if (playersEdgesPath != null)
        {



            if (currentEdgeIndex + 1 < playersEdgesPath.Count)
            {
                currentEdgeIndex++;


                currentAStarEdge = playersEdgesPath[currentEdgeIndex];
                //Player_EventManager.instance.InitialisePossibleEvent(edgesPath[currentEdgeIndex]);

                currentGoalNode = playersEdgesPath[currentEdgeIndex].ReturnOtherEndOfPath(currentGoalNode);


                isForward = playersEdgesPath[currentEdgeIndex].headNode == currentGoalNode;
                currentDistanceTravelledOnEdge = isForward ? 0 : playersEdgesPath[currentEdgeIndex].LScore;
            }
            else
            {
                trailRenderer.time = startingTrailRendererTime * .6f;
                if (delayTimer >= delayBetweenRuns)
                {
                    delayTimer = 0;
                    Player_FollowBezierPath.instance.UpdateBreadcrumb();
                }
            }
        }
    }

    public void CancelJourney()
    {
        ResetEdgesPathList();
        ChangeMovementState(MovementState.AWAITING_INSTRUCTION);
        ResetCrumb();
    }


    private void ChangeMovementState(MovementState movementState)
    {
        this.currentMovementState = movementState;
    }


    private void ResetEdgesPathList()
    {
        playersEdgesPath = null;
        currentEdgeIndex = 0;
    }
}
