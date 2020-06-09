using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class PathfindingManagementWindow : EditorWindow
{   // Commented out to remove from editor.
    /*
    [MenuItem("Tools/Pathfinding Management Window")]
    public static void Open()
    {
        GetWindow<PathfindingManagementWindow>();
    }
    int nodeIndex = 0;
    public GameObject pointPrefab;
    public AStarNode AStarNodePrefab;

    public AStarEdge selectedAStarEdge;

    public bool isEditMode = false;

    private void OnGUI()
    {
        // EDGE.
        SerializedObject objEdge = new SerializedObject(this);

        EditorGUILayout.PropertyField(objEdge.FindProperty("selectedAStarEdge"));

        if (selectedAStarEdge == null)
        {
            EditorGUILayout.HelpBox("An AstarEdge must be selected. Please assign an AStarEdge to modify", MessageType.Warning);
        }
        // POINT
        SerializedObject objPrefab = new SerializedObject(this);

        EditorGUILayout.PropertyField(objPrefab.FindProperty("pointPrefab"));

        if (pointPrefab == null)
        {
            EditorGUILayout.HelpBox("A prefab Point must be set. Please assign a prefab Point", MessageType.Warning);
        }
        // NODE.
        SerializedObject objNode = new SerializedObject(this);

        EditorGUILayout.PropertyField(objNode.FindProperty("AStarNodePrefab"));

        if (AStarNodePrefab == null)
        {
            EditorGUILayout.HelpBox("A prefab AStarNode must be set. Please assign a prefab AStarNode", MessageType.Warning);
        }

        // NODE INDEX.
        nodeIndex = EditorGUILayout.IntField("Next Node Index:", nodeIndex);

        EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();


        objEdge.ApplyModifiedProperties();
        objPrefab.ApplyModifiedProperties();
        objNode.ApplyModifiedProperties();
    }



    void DrawButtons()
    {
        isEditMode = GUILayout.Toggle(isEditMode, "EditMode - Toggle off when not editing.");

        if (isEditMode && Selection.activeGameObject != null)
        {
            //Set Selected Edge As Selected Edge.
            if (Selection.activeGameObject.GetComponent<AStarEdge>())
            {
                if (GUILayout.Button("Set Selected Edge As Selected Edge"))
                {
                    SetSelectedEdgeAsSelectedEdge();
                }
            }
            //Set Selected Points Parent As Selected Edge.
            else if (Selection.activeGameObject.transform.parent != null && Selection.activeGameObject.transform.parent.GetComponent<AStarEdge>() != null)
            {
                if (selectedAStarEdge != null && Selection.activeGameObject.transform.parent == selectedAStarEdge.transform)
                {
                    if (GUILayout.Button("Insert New Point Before"))
                    {
                        InsertNewPoint(0);
                    }
                    if (GUILayout.Button("Insert New Point After"))
                    {
                        InsertNewPoint(1);
                    }
                    if (selectedAStarEdge.transform.childCount > 1)
                    {
                        if (GUILayout.Button("Remove Selected Point"))
                        {
                            RemoveSelectedPoint();
                        }
                    }
                    if (GUILayout.Button("Bake Child NamesOrder"))
                    {
                        BakeChildNamesOrder();
                    }
                    if (GUILayout.Button("Convert Point To Node"))
                    {
                        ConvertPointToNode();
                    }
                }
                else
                {
                    if (GUILayout.Button("Set Selected Points Parent As Selected Edge"))
                    {
                        SetSelectedPointsParentAsSelectedEdge();
                    }
                }
            }
            
            if (Selection.activeGameObject.GetComponent<AStarNode>())
            {
                if (GUILayout.Button("Create New Node Linked To Selected Node"))
                {
                    CreateNewNodeLinkedToSelectedNode();
                }
            }
        }
    }

    void SetSelectedEdgeAsSelectedEdge()
    {
        selectedAStarEdge = Selection.activeGameObject.GetComponent<AStarEdge>();
    }
    void SetSelectedPointsParentAsSelectedEdge()
    {
        selectedAStarEdge = Selection.activeGameObject.transform.GetComponentInParent<AStarEdge>();
    }
    void InsertNewPoint(int siblinfIndexOffset, bool isNewSelectedObject = true)
    {
        GameObject pointObject = Instantiate(pointPrefab, selectedAStarEdge.transform);
        pointObject.transform.name = selectedAStarEdge.transform.childCount.ToString();

        Transform selectedPointTransform = Selection.activeGameObject.transform;

        pointObject.transform.position = selectedPointTransform.transform.position + (selectedPointTransform.transform.up * 0.1f);
        pointObject.transform.forward = selectedPointTransform.transform.forward;

        pointObject.transform.SetSiblingIndex(selectedPointTransform.transform.GetSiblingIndex() + siblinfIndexOffset);

        if (isNewSelectedObject)
        {
            Selection.activeGameObject = pointObject.gameObject;
        }

        BakeChildNamesOrder();
    }

    void RemoveSelectedPoint()
    {
        int childIndex = Selection.activeGameObject.transform.GetSiblingIndex();
        if (childIndex == selectedAStarEdge.transform.childCount-1)
        { childIndex--; }

        DestroyImmediate(Selection.activeGameObject.transform.gameObject);
  

            Selection.activeGameObject = selectedAStarEdge.transform.GetChild(childIndex).gameObject;

        BakeChildNamesOrder();
    }
    void BakeChildNamesOrder()
    {
        int childIndex = 0;
        string pointName = "Point";
        foreach (Transform child in selectedAStarEdge.transform)
        {
            child.name = pointName + childIndex++.ToString();
        }
    }
    void ConvertPointToNode()
    {
        Transform originalPointGameObject = Selection.activeGameObject.transform;
        selectedAStarEdge = originalPointGameObject.transform.GetComponentInParent<AStarEdge>();

        AStarNode newNode = Instantiate(AStarNodePrefab, selectedAStarEdge.transform.parent) as AStarNode;
        newNode.name = "Node" + nodeIndex++;

        newNode.transform.position = originalPointGameObject.position;
        newNode.transform.forward = originalPointGameObject.forward;

        AStarNode oldheadNode = selectedAStarEdge.headNode;
        AStarNode oldTailNode = selectedAStarEdge.tailNode;


        GameObject newEdgeObj = new GameObject("Path " + newNode.transform.name + "," + oldheadNode.transform.name, typeof(AStarEdge));
        newEdgeObj.transform.SetParent(selectedAStarEdge.transform.parent);
        AStarEdge newEdge = newEdgeObj.GetComponent<AStarEdge>();

        selectedAStarEdge.name = "Path " + oldTailNode.transform.name + "," + newNode.transform.name;

        selectedAStarEdge.headNode = newNode;
        newEdge.headNode = oldheadNode;
        newEdge.tailNode = newNode;
       

        newNode.connectingEdges.Add(newEdge);
        newNode.connectingEdges.Add(selectedAStarEdge);

        oldheadNode.connectingEdges.Remove(selectedAStarEdge);
        oldheadNode.connectingEdges.Add(newEdge);

        BakeChildNamesOrder();

        int selectedPointSiblingIndex = originalPointGameObject.GetSiblingIndex();



        if (selectedPointSiblingIndex == selectedAStarEdge.transform.childCount - 1)
        {
            InsertNewPoint(1);
        }

        if (selectedPointSiblingIndex != 0)
        {
            if(selectedAStarEdge.transform.childCount > 2)
            {
                for (int i = 0; i < selectedPointSiblingIndex; i++)
                {
                    selectedAStarEdge.transform.GetChild(i).SetParent(newEdge.transform);

                }
            }
        }
        else
        {
            selectedAStarEdge = newEdge;
            InsertNewPoint(0);
        }

        DestroyImmediate(originalPointGameObject.gameObject);

        Selection.activeGameObject = newNode.gameObject;

    }
    void CreateNewNodeLinkedToSelectedNode()
    {

        Transform selectedNodeTransform = Selection.activeGameObject.transform;

        // NEW NODE.
        AStarNode newNode = Instantiate(AStarNodePrefab, selectedNodeTransform.parent) as AStarNode;
        newNode.name = "Node" + nodeIndex++;

        newNode.transform.position = selectedNodeTransform.position + (selectedNodeTransform.up * 0.2f);
        newNode.transform.forward = selectedNodeTransform.forward;

        AStarNode existingNode = selectedNodeTransform.GetComponent<AStarNode>();

        // NEW EDGE.
        GameObject newEdgeObj = new GameObject("Path " + newNode.transform.name + "," + Selection.activeGameObject.transform.name, typeof(AStarEdge));
        newEdgeObj.transform.SetParent(selectedNodeTransform.parent);
        AStarEdge newEdge = newEdgeObj.GetComponent<AStarEdge>();
        selectedAStarEdge = newEdge;

        newEdge.headNode = newNode;
        newEdge.tailNode = existingNode;

        newNode.connectingEdges.Add(newEdge);
        existingNode.connectingEdges.Add(newEdge);

        InsertNewPoint(0, false);

        Selection.activeGameObject = newNode.gameObject;

    }
    */
}
