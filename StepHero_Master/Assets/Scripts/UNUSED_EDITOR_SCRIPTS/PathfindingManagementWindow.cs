using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class PathfindingManagementWindow : EditorWindow
{   // Commented out to remove from editor.
    
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
        GameObject newEdgeObj = new GameObject("PathCreator " + newNode.transform.name + "," + existingNode.transform.name, typeof(AStarEdge));
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
    
}

/*
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
            lScore += Vector2.Distance(transform.GetChild(transform.childCount - 1).transform.position, tailNode.transform.position);
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
        if (end == headNode)
        {
            return tailNode;
        }
        return headNode;

    }

    public void SetNodeConnections(AStarNode headNode, AStarNode tailNode)
    {

    }

   
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
    }
}

public class AStarNode : MonoBehaviour
{

    public AStarNode previous;

    // BELOW MIGHT WORK?????
    public int bestEdgeIndex = 0;
    public int previousBestEdgeIndex = 0;


    public float gScore;
    public float hScore;

    public float fScore { get { return gScore + hScore; } }

    public int id;


    //- These are the nodes this node connects too, index 0 is always the node its own path leads to.
    public List<AStarEdge> connectingEdges = new List<AStarEdge>();

    public void ResetNode()
    {
        this.gScore = 999999;
        this.previous = null;
    }
}



 // This would be placed in the Editor folder.
 
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
//- This AStar code and node system is based off SebLague Lagues A* found at.
// https://github.com/SebLague/Pathfinding/tree/master/Episode%2003%20-%20astar/Assets/Scripts
// - For references to FindClosestWaypoint for when player is selecting waypoints to move to try.
// http://www.trickyfast.com/2017/09/21/building-a-waypoint-pathing-system-in-unity/

public class AStarPathfinder : MonoBehaviour
{
	public AStarNode start, goal;
	public List<AStarNode> nodePath = new List<AStarNode>();
	public List<Vector2> pointPath = new List<Vector2>();

	public float maxDistancePerGoal = 50f;

	public float totalPathDistance = 0;

	void Start()
	{
		FindPath(start, goal);
	}




	void FindPath(AStarNode start, AStarNode target)
	{

		List<AStarNode> openSet = new List<AStarNode>();
		HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			AStarNode node = openSet[0];
			AStarNode otherNode = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fScore < node.fScore || openSet[i].fScore == node.fScore)
				{
					if (openSet[i].hScore < node.hScore)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == target)
			{
				RetraceAndReversePath(start, target);
				return;
			}
            for (int i = 0; i < node.connectingEdges.Count; i++)
            {
				otherNode = node.connectingEdges[i].ReturnOtherEndOfPath(node);
				if (closedSet.Contains(otherNode))
				{
					continue;
                }

				//float newGScore = node.gScore + Vector2.Distance(node.position, node.connectingNodes[i].position);
				float newGScore = node.gScore + node.connectingEdges[i].LScore;
				if (newGScore < otherNode.gScore || !openSet.Contains(otherNode))
				{
					otherNode.gScore = newGScore;
					otherNode.hScore = Vector2.Distance(otherNode.transform.position, target.transform.position);
					otherNode.previous = node;
					otherNode.bestEdgeIndex = i;
                    // WORKS
					//otherNode.previousBestEdgeIndex = i;
					//node.bestEdgeIndex = i;

					if (!openSet.Contains(otherNode))
						openSet.Add(otherNode);
				}

			}
            // FROM HERE  TO BELOW  IS OBSOLETE. vvvvvv
			foreach (MyAStarNode neighbour in node.connectingNodes)
			{
				if (closedSet.Contains(neighbour))
				{
					continue;
				}

				float newGScore = node.gScore + Vector2.Distance(node.position, neighbour.position);
				if (newGScore < neighbour.gScore || !openSet.Contains(neighbour))
				{
					neighbour.gScore = newGScore;
					neighbour.hScore = Vector2.Distance(neighbour.position, target.position);
					neighbour.previous = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
            // FROM HERE  TO ABOVE  IS OBSOLETE. ^^^^^^
		}
	}

    // FROM HERE  TO BELOW  IS OBSOLETE. vvvvvv
	void RetracePathe(MyAStarNode startNode, MyAStarNode endNode)
	{

		MyAStarNode currentNode = endNode;

		while (currentNode != startNode)
		{
			nodePath.Add(currentNode);

			if (currentNode.previous != currentNode.connectingNodes[0])
			{
				currentNode.previous.connectingPathIsThis = true;

			}
			currentNode = currentNode.previous;
		}
		nodePath.Reverse();
		foreach (var item in nodePath)
		{
			print(item.gameObject.name + " " + item.fScore);
		}
		RetracePathToConstructWaypointPath(startNode, endNode);
	}
    // FROM HERE  TO ABOVE  IS OBSOLETE. ^^^^^^


void RetraceAndReversePath(AStarNode startNode, AStarNode endNode)
{

    AStarNode currentNode = endNode;
    int currentBest = currentNode.bestEdgeIndex;
    int previousBest = 0;

    while (currentNode != startNode)
    {
        nodePath.Add(currentNode);

        previousBest = currentNode.previous.bestEdgeIndex;

        currentNode = currentNode.previous;

        currentNode.bestEdgeIndex = currentBest;

        currentBest = previousBest;

    }
    nodePath.Reverse();

    RetracePathToConstructWaypointPath(startNode, endNode);
}

void RetracePathToConstructWaypointPath(AStarNode startNode, AStarNode endNode)
{

    pointPath.AddRange(startNode.connectingEdges[startNode.bestEdgeIndex].GetCorrectDirectionPathPoints(startNode));
    //startNode.ResetNode();
    for (int i = 0; i < nodePath.Count - 1; i++)
    {
        pointPath.AddRange(nodePath[i].connectingEdges[nodePath[i].bestEdgeIndex].GetCorrectDirectionPathPoints(nodePath[i]));
        //nodePath[i].ResetNode();
    }
    //endNode.ResetNode();
    print("DONE!");
    //totalPathDistance = endNode.gScore;
    //print("totalPathDistance " + totalPathDistance);

    print(Time.realtimeSinceStartup);


}
// FROM HERE  TO BELOW  IS OBSOLETE. vvvvvv
void RetracePathToConstructWaypointPath(MyAStarNode startNode, MyAStarNode endNode)
	{
		if (startNode.connectingPathIsThis)
		{
			foreach (WaypointDumb item in startNode.path)
			{
				waypointPath.Add(item);
			}
		}
		else
		{
			for (int i = nodePath[0].path.Count - 2; i > -1; i--)
			{
				waypointPath.Add(nodePath[0].path[i]);
			}
			waypointPath.Add(nodePath[0].thisWaypoint);
		}

        for (int i = 0; i < nodePath.Count; i++)
        {
			if (nodePath[i].connectingPathIsThis)
			{
				foreach (WaypointDumb item in nodePath[i].path)
				{
					waypointPath.Add(item);
				}
			}
			else
            {
				if (i < (nodePath.Count - 1))
				{
                    for (int n = nodePath[i + 1].path.Count-2; n > -1; n--)
                    {
						waypointPath.Add(nodePath[i + 1].path[n]);
                    }
					waypointPath.Add(nodePath[i + 1].thisWaypoint);
				}
            }
        }
		print("DONE!");
		totalPathDistance = endNode.gScore;
		print("totalPathDistance " + totalPathDistance);
	}
    // FROM HERE  TO ABOVE  IS OBSOLETE. ^^^^^^
}



     */
