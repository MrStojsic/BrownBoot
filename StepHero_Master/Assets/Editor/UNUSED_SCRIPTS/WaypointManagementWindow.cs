using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManagementWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Management Window")]
    public static void Open()
    {
        GetWindow<WaypointManagementWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (waypointRoot != null && waypointRoot.childCount == 0)
        {
            if (GUILayout.Button("Create Initial Waypoint"))
            {
                CreateInitialWaypoint();
            }
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Insert New Waypoint Before"))
            {
                InsertNewWaypointBefore();
            }
            if (GUILayout.Button("Insert New Waypoint After"))
            {
                InsertNewWaypointAfter();
            }
            if (GUILayout.Button("Remove Selected Waypoint"))
            {
                RemoveSelectedWaypoint();
            }
        }
        if (waypointRoot != null && waypointRoot.childCount > 1)
        {
            if (GUILayout.Button("Bake Waypoint Order"))
            {
                BakeWaypointOrder();
            }
        }
    }

    void CreateInitialWaypoint()
    {
        GameObject newWaypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));

        newWaypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = newWaypointObject.GetComponent<Waypoint>();

        if (waypointRoot.childCount > 1)
        {
            newWaypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            newWaypoint.previousWaypoint.nextWaypoint = newWaypoint;

            newWaypoint.transform.position = newWaypoint.previousWaypoint.transform.position;
            newWaypoint.transform.forward = newWaypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void InsertNewWaypointBefore()
    {
        GameObject newWaypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        newWaypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = newWaypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        newWaypointObject.transform.position = selectedWaypoint.transform.position;
        newWaypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;

        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;


    }
    void InsertNewWaypointAfter()
    {
        GameObject newWaypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        newWaypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = newWaypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        newWaypointObject.transform.position = selectedWaypoint.transform.position;
        newWaypointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.nextWaypoint != null)
        {
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;

        }

        newWaypoint.previousWaypoint = selectedWaypoint;
        selectedWaypoint.nextWaypoint = newWaypoint;

        if (selectedWaypoint.nextWaypoint != null)
        {
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex()+1);
        }
        else
        {
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        }
        Selection.activeGameObject = newWaypoint.gameObject;


    }
    void RemoveSelectedWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }
    void BakeWaypointOrder()
    {
        Debug.Log(waypointRoot.transform.GetChild(0).GetComponent<Waypoint>().BakeTotalPathDistance(0));
        if (waypointRoot.childCount > 0)
        {
            for (int i = 0; i < waypointRoot.childCount-1; i++)
            {
                waypointRoot.GetChild(i).GetComponent<Waypoint>().nextWaypoint.transform.SetSiblingIndex(i + 1);
            }

        }
    }
}