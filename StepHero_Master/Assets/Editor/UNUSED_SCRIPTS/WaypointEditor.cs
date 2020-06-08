using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(Waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.magenta;
        }
        else
        {
            Gizmos.color = Color.magenta * 0.5f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, .1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * .2f),
            waypoint.transform.position - (waypoint.transform.right * .2f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(waypoint.transform.position, waypoint.transform.position + (waypoint.transform.up * .2f));

        if (waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offsetThis = waypoint.transform.right * .2f;
            Vector3 offsetPrevious = waypoint.previousWaypoint.transform.right * .2f;

            Gizmos.DrawLine(waypoint.transform.position + offsetThis, waypoint.previousWaypoint.transform.position + offsetPrevious);
        }

        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 offsetThis = waypoint.transform.right * -.2f;
            Vector3 offsetNext = waypoint.nextWaypoint.transform.right * -.2f;

            Gizmos.DrawLine(waypoint.transform.position + offsetThis, waypoint.nextWaypoint.transform.position + offsetNext);
        }
    }
}
