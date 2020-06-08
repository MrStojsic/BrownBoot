using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class PathfindingEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(AStarEdge aStarEdge, GizmoType gizmoType)
    {
        if (aStarEdge.transform.childCount > 1)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < aStarEdge.transform.childCount - 1; i++)
            {
                Gizmos.DrawLine(aStarEdge.transform.GetChild(i).transform.position, aStarEdge.transform.GetChild(i + 1).transform.position);
            }
        }
        if (aStarEdge.transform.childCount > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(aStarEdge.headNode.transform.position, aStarEdge.transform.GetChild(0).transform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(aStarEdge.transform.GetChild(aStarEdge.transform.childCount - 1).transform.position, aStarEdge.tailNode.transform.position);

        }
        else
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(aStarEdge.headNode.transform.position, aStarEdge.tailNode.transform.position);
        }
    }

}
