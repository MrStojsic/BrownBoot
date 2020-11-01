using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PathCreation;
using PathCreationEditor;

public class PathfindingPlacementManagementWindow : EditorWindow
{
    [MenuItem("Tools/Pathfinding Placement Management Window")]
    public static void Open()
    {
        GetWindow<PathfindingPlacementManagementWindow>();
    }
    public bool isEditMode = false;

    int nodeIndex = 0;
    public AStarNode pfAStarNode;
    public PathCreator pfPathCreator;

    [Header("Parents")]
    public Transform nodesParent;
    public Transform pathsParent;


    private void OnGUI()
    {
        // NODE.
        SerializedObject objNode = new SerializedObject(this);

        EditorGUILayout.PropertyField(objNode.FindProperty("pfAStarNode"));

        if (pfAStarNode == null)
        {
            EditorGUILayout.HelpBox("A prefab AStarNode must be set. Please assign a prefab AStarNode", MessageType.Warning);
        }
        // PATH CREATOR.
        SerializedObject objPath = new SerializedObject(this);

        EditorGUILayout.PropertyField(objPath.FindProperty("pfPathCreator"));

        if (pfPathCreator == null)
        {
            EditorGUILayout.HelpBox("A prefab PathCreator must be set. Please assign a prefab PathCreator", MessageType.Warning);
        }

        objNode.ApplyModifiedProperties();
        objPath.ApplyModifiedProperties();

        // NODE INDEX.
        nodeIndex = EditorGUILayout.IntField("Next Node Index:", nodeIndex);

        // NODE & PATH PARENTS.
        SerializedObject objNodeParent = new SerializedObject(this);
        EditorGUILayout.PropertyField(objNodeParent.FindProperty("nodesParent"));
        objNodeParent.ApplyModifiedProperties();

        SerializedObject objPathParent = new SerializedObject(this);
        EditorGUILayout.PropertyField(objPathParent.FindProperty("pathsParent"));
        objPathParent.ApplyModifiedProperties();


        EditorGUILayout.BeginVertical("box");
        DrawButtons();
        EditorGUILayout.EndVertical();
    }

    void DrawButtons()
    {
        isEditMode = GUILayout.Toggle(isEditMode, "EditMode - Toggle off when not editing.");
  
        if (isEditMode && Selection.activeGameObject != null)
        {
            if (Selection.activeGameObject.GetComponent<AStarNode>())
            {
                GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("Create New Node Bézier Linked To Selected Node"))
                {
                    CreateNewNodeBezierLinkedToSelectedNode();
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove Selected Node And All Connected Bezier Paths"))
                {
                    RemoveSelectedNodeAndAllConnectedBezierPaths();
                }
            }
            // TODO Impliment adding a node halfway along a bezier path.. Maybe?
            if (Selection.activeGameObject.GetComponent<AStarEdge>())
            {
                if (GUILayout.Button("Insert New Node At Path Head"))
                {
                    InsertNewNodeAtPathHead();
                }
                if (GUILayout.Button("Insert New Node At Path Tail"))
                {
                    InsertNewNodeAtPathTail();
                }
                if (GUILayout.Button("Remove Selected Connected Bezier Path"))
                {
                    RemoveSelectedConnectedBezierPath();
                }
            }
        }
    }

    void CreateNewNodeBezierLinkedToSelectedNode()
    {
        Transform selectedNodeTransform = Selection.activeGameObject.transform;

        // NEW NODE.
        AStarNode newNode = Instantiate(pfAStarNode, nodesParent) as AStarNode;
        newNode.name = "Node" + nodeIndex++;
        newNode.transform.position = selectedNodeTransform.position + (selectedNodeTransform.up * 0.5f);
        newNode.transform.forward = selectedNodeTransform.forward;

        AStarNode existingNode = selectedNodeTransform.GetComponent<AStarNode>();

        // NEW PATH EDGE.
        PathCreator pathEdge = Instantiate(pfPathCreator, pathsParent) as PathCreator;
        AStarEdge newEdge = pathEdge.transform.gameObject.AddComponent<AStarEdge>();
        newEdge.pathCreator = pathEdge;
        newEdge.transform.name = "Path - " + selectedNodeTransform.name + " > " + newNode.name;

        // CONFIGURE PATH.
        pathEdge.bezierPath = new BezierPath((newNode.transform.position + (existingNode.transform.position - newNode.transform.position) / 2), false, PathSpace.xyz);
        pathEdge.bezierPath.MovePoint(0, existingNode.transform.position);
        pathEdge.bezierPath.MovePoint(3, newNode.transform.position);

        // LINK NODES AND PATHS.
        newEdge.headNode = newNode;
        newEdge.tailNode = existingNode;
        newNode.connectingEdges.Add(newEdge);
        existingNode.connectingEdges.Add(newEdge);
 
        Selection.activeGameObject = newNode.gameObject;

    }

    void RemoveSelectedNodeAndAllConnectedBezierPaths()
    {
        AStarNode aStarNode = Selection.activeGameObject.GetComponent<AStarNode>();
        AStarNode otherAStarNode;
        AStarEdge aStarEdge;
        for (int i = aStarNode.connectingEdges.Count-1; i > -1; i--)
        {
            Debug.Log(aStarNode.name + " " + (aStarNode.connectingEdges.Count - 1));
            
            otherAStarNode = aStarNode.connectingEdges[i].ReturnOtherEndOfPath(aStarNode);
            for (int j = 0; j < otherAStarNode.connectingEdges.Count; j++)
            {

                Debug.Log(j +" <O , T> " + i);
                if (otherAStarNode.connectingEdges[j] == aStarNode.connectingEdges[i])
                {
                    aStarEdge = otherAStarNode.connectingEdges[j];
                    otherAStarNode.connectingEdges.RemoveAt(j);
                    DestroyImmediate(aStarEdge.gameObject);
                }
            }
        }
        DestroyImmediate(aStarNode.gameObject);

    }
    void InsertNewNodeAtPathHead()
    {
        // GET ORIGINAL NODE AND EDGE THEN DISCONNECT.
        AStarEdge originalEdge = Selection.activeGameObject.GetComponent<AStarEdge>();
        AStarNode nodeToRepath = originalEdge.headNode;
        nodeToRepath.connectingEdges.Remove(originalEdge);

        // NEW NODE.
        AStarNode newNode = Instantiate(pfAStarNode, nodesParent) as AStarNode;
        newNode.name = "Node" + nodeIndex++;
        newNode.transform.forward = nodeToRepath.transform.forward;
        float offset = originalEdge.transform.position.y >= nodeToRepath.transform.position.y ? 0.5f : -0.5f;
        newNode.transform.position = nodeToRepath.transform.position + (nodeToRepath.transform.up * offset);
        originalEdge.headNode = newNode;
        newNode.connectingEdges.Add(originalEdge);


        // LINK ORIGINAL PATH TO NEW NODE.
        PathCreator originalPathCreator = originalEdge.transform.GetComponent<PathCreator>();
        originalPathCreator.bezierPath.MovePoint(originalPathCreator.bezierPath.NumPoints - 1, newNode.transform.position);
        originalEdge.transform.name = "Path - " + originalEdge.tailNode.name + " > " + newNode.name;


        // NEW PATH EDGE.
        PathCreator newPathCreator = Instantiate(pfPathCreator, pathsParent) as PathCreator;
        AStarEdge newEdge = newPathCreator.transform.gameObject.AddComponent<AStarEdge>();
        newEdge.pathCreator = newPathCreator;
        newEdge.transform.name = "Path - " + newNode.name + " > " + nodeToRepath.name;

        // CONFIGURE PATH.
        newPathCreator.bezierPath = new BezierPath((newNode.transform.position + (nodeToRepath.transform.position - newNode.transform.position) / 2), false, PathSpace.xyz);
        newPathCreator.bezierPath.MovePoint(0, newNode.transform.position);
        newPathCreator.bezierPath.MovePoint(3, nodeToRepath.transform.position);

        // LINK NODES AND PATHS.
        newNode.connectingEdges.Add(newEdge);
        nodeToRepath.connectingEdges.Add(newEdge);
        newEdge.tailNode = newNode;
        newEdge.headNode = nodeToRepath;
    }

    void InsertNewNodeAtPathTail()
    {
        // GET ORIGINAL NODE AND EDGE THEN DISCONNECT.
        AStarEdge originalEdge = Selection.activeGameObject.GetComponent<AStarEdge>();
        AStarNode nodeToRepath = originalEdge.tailNode;
        nodeToRepath.connectingEdges.Remove(originalEdge);

        // NEW NODE.
        AStarNode newNode = Instantiate(pfAStarNode, nodesParent) as AStarNode;
        newNode.name = "Node" + nodeIndex++;
        newNode.transform.forward = nodeToRepath.transform.forward;
        float offset = originalEdge.transform.position.y >= nodeToRepath.transform.position.y ? 0.5f : -0.5f;
        newNode.transform.position = nodeToRepath.transform.position + (nodeToRepath.transform.up * offset);
        originalEdge.tailNode = newNode;
        newNode.connectingEdges.Add(originalEdge);


        // LINK ORIGINAL PATH TO NEW NODE.
        PathCreator originalPathCreator = originalEdge.transform.GetComponent<PathCreator>();
        originalPathCreator.bezierPath.MovePoint(0, newNode.transform.position);
        originalEdge.transform.name = "Path - " + originalEdge.headNode.name + " > " + newNode.name;


        // NEW PATH EDGE.
        PathCreator newPathCreator = Instantiate(pfPathCreator, pathsParent) as PathCreator;
        AStarEdge newEdge = newPathCreator.transform.gameObject.AddComponent<AStarEdge>();
        newEdge.pathCreator = newPathCreator;
        newEdge.transform.name = "Path - " + newNode.name + " > " + nodeToRepath.name;

        // CONFIGURE PATH.
        newPathCreator.bezierPath = new BezierPath((newNode.transform.position + (nodeToRepath.transform.position - newNode.transform.position) / 2), false, PathSpace.xyz);
        newPathCreator.bezierPath.MovePoint(0, nodeToRepath.transform.position);
        newPathCreator.bezierPath.MovePoint(3, newNode.transform.position);

        // LINK NODES AND PATHS.
        newNode.connectingEdges.Add(newEdge);
        nodeToRepath.connectingEdges.Add(newEdge);
        newEdge.tailNode = nodeToRepath;
        newEdge.headNode = newNode;
    }
    void RemoveSelectedConnectedBezierPath()
    {
        // GET EDGE, DISCONNECT FROM NODES, DESTORY IT.
        AStarEdge aStarEdge = Selection.activeGameObject.GetComponent<AStarEdge>();
        aStarEdge.headNode.connectingEdges.Remove(aStarEdge);
        aStarEdge.tailNode.connectingEdges.Remove(aStarEdge);
        DestroyImmediate(aStarEdge.gameObject);
    }
}
