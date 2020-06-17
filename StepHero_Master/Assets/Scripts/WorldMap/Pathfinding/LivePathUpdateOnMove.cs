using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class LivePathUpdateOnMove : MonoBehaviour
{
    [SerializeField]
    AStarNode aStarNode;
    void Awake()
    {
        Debug.Log("Editor causes this Awake");
    }

    void Update()
    {
        
        foreach (var item in aStarNode.connectingEdges)
        {
            if (aStarNode == item.headNode)
            {
                item.pathCreator.bezierPath.MovePoint(item.pathCreator.bezierPath.NumPoints - 1, this.transform.position);
            }
            else
            {
                item.pathCreator.bezierPath.MovePoint(0, this.transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green*.7f;
        Gizmos.DrawSphere(this.transform.position, .07f);

    }
}


