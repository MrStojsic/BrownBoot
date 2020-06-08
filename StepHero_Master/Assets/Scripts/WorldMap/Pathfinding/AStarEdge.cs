using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEdge : MonoBehaviour
{
    public AStarNode headNode = null;
    public AStarNode tailNode = null;

    //- This is the lenght of this nodes path that leads to its 0 index connectingNode.
    public float lScore = 0;
    public float LScore
    {
        get { return lScore; }
    }
    
    private void Awake()
    {
        CalculateCost();
    }

    
    public void CalculateCost()
    {
        if (transform.childCount > 0)
        {
            lScore += Vector2.Distance(headNode.transform.position, transform.GetChild(0).transform.position);
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                lScore += Vector2.Distance(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
            }
            lScore += Vector2.Distance(transform.GetChild(transform.childCount-1).transform.position, tailNode.transform.position);
        }
        else
        {
            lScore = Vector2.Distance(headNode.transform.position, tailNode.transform.position);
        }
        //print(lScore);
    }


    public Vector2[] GetCorrectDirectionPathPoints(AStarNode nextNode)
    {
        bool isHeadNode = (nextNode == headNode);

        Vector2[] pathPoints = new Vector2[transform.childCount + 1];

        if (isHeadNode)
        {
            pathPoints[transform.childCount] = tailNode.transform.position;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    pathPoints[i] = transform.GetChild(i).transform.position;
                }
            }
        }
        else
        {
            pathPoints[transform.childCount] = headNode.transform.position;
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    pathPoints[i] = transform.GetChild(transform.childCount - i - 1).transform.position;
                }
            }
        }
        return pathPoints;
    }

    public AStarNode ReturnOtherEndOfPath(AStarNode end)
    {
        if(end == headNode)
        {
            return tailNode;
        }
        return headNode;
        
    }

    public void SetNodeConnections(AStarNode headNode, AStarNode tailNode)
    {

    }

    /*
    private void OnDrawGizmos()
    {
        if (transform.childCount == 0)
        {
            return;
        }

        Gizmos.color = Color.black;

        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.GetChild(0).transform.position, headNode.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.GetChild(transform.childCount-1).transform.position, tailNode.transform.position);
    }*/
}
