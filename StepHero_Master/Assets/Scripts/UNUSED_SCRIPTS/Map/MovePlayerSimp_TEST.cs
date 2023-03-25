using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerSimp_TEST : MonoBehaviour
{
    public float speed = 3f;
    public bool doMove = true;

    public Vector2 goalPosition;
    public Vector2 actualGoalPosition;

    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();
    public Waypoint currentWaypoint;
    [SerializeField] private int currentWaypointIndex = -1;

    public float percentage = .5f;
    public float distanceInUnityUnits;

    public bool isDebug = false;
    public int steps;
    public int mapMetersPerUnit = 1000;
    float stepScalar = 1f;// <- is just a test value, Actual stepScalar should be 1.333333333333f;


    // Start is called before the first frame update
    void Start()
    {
        if(!isDebug)
        {
            distanceInUnityUnits = CalculateUnityDistanceFromSteps(steps, stepScalar, mapMetersPerUnit);
        }

        if (waypoints.Count > 0)
        {
            SetNextWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doMove)
        {
            MoveTowardsGoalWaypoint();
        }
    }

    private void MoveTowardsGoalWaypoint()
    {
        float step = speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, actualGoalPosition) > 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, actualGoalPosition, step);
        }
        else
        {
            if (percentage == 1)
            {
                SetNextWaypoint();
            }
            else
            {
                doMove = false;
            }
        }
    }

    private void SetNextWaypoint()
    {
        if (currentWaypointIndex + 1 < waypoints.Count)
        {
            currentWaypointIndex ++;

            currentWaypoint = waypoints[currentWaypointIndex];
            goalPosition = currentWaypoint.transform.position;
            actualGoalPosition = Vector2.Lerp(transform.position, goalPosition, CalculatePercentageRemaining());
        }
    }

    private float CalculatePercentageRemaining()
    {
        float distanceToGoal = Vector2.Distance(transform.position, goalPosition);

        if (distanceToGoal <= distanceInUnityUnits)
        {
            percentage = 1f;
            distanceInUnityUnits -= distanceToGoal;
        }
        else
        {
            percentage = distanceInUnityUnits / distanceToGoal;
            distanceInUnityUnits = 0;
        }

        return percentage;
    }

    public int CalculateStepsFromUnityDistance(float _distanceInUnityUnits, int _mapMetersPerUnit, float _stepScalar)
    {
        // 0.075 * 1000 * 1.333333333333 = 100
        return (int)Mathf.Round(_distanceInUnityUnits * _mapMetersPerUnit * _stepScalar);
    }

    public float CalculateUnityDistanceFromSteps(int _steps, float _stepScalar, int _mapMetersPerUnit)
    {
        // 100/1.333333333333/1000 = 0.075
        return (float)(_steps / _stepScalar / _mapMetersPerUnit);
    }
}


