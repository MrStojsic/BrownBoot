using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
//- This AStar code and node system is based off SebLague Lagues A* found at.
// https://github.com/SebLague/Pathfinding/tree/master/Episode%2003%20-%20astar/Assets/Scripts
// - For references to FindClosestWaypoint for when player is selecting waypoints to move to try.
// http://www.trickyfast.com/2017/09/21/building-a-waypoint-pathing-system-in-unity/

public class AStarPathfinder : MonoBehaviour
{
	public AStarNode start, goal;
	public List<AStarNode> nodePath = new List<AStarNode>();
	public List<AStarEdge> finalEdgePath = new List<AStarEdge>();

	public float maxDistancePerGoal = 50f; // UNUSED.

	public List<AStarEdge> FindPath(AStarNode startNode, AStarNode destinationNode)
	{

		List<AStarNode> openSet = new List<AStarNode>();
		HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
		openSet.Add(startNode);

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

			if (node == destinationNode)
			{
				RetraceAndReversePath(startNode, destinationNode);
				return finalEdgePath;
			}
            for (int i = 0; i < node.connectingEdges.Count; i++)
            {
				if (node.connectingEdges[i].isTraversable)
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
						otherNode.hScore = Vector2.Distance(otherNode.transform.position, destinationNode.transform.position);
						otherNode.previous = node;
						otherNode.bestEdgeIndex = i;
						// WORKS
						//otherNode.previousBestEdgeIndex = i;
						//node.bestEdgeIndex = i;

						if (!openSet.Contains(otherNode))
							openSet.Add(otherNode);
					}
				}

			}
            /*
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
			}*/
		}
		return null;
	}

	/*void RetracePathe(MyAStarNode startNode, MyAStarNode endNode)
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
	}*/


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

		RetracePathToConstructWaypointPath(startNode,endNode);
	}
    
	void RetracePathToConstructWaypointPath(AStarNode startNode, AStarNode endNode)
	{

		finalEdgePath.Add(startNode.connectingEdges[startNode.bestEdgeIndex]);
		startNode.ResetNode();
        for (int i = 0; i < nodePath.Count-1; i++)
        {
			// TODO. This currently returns the pathCreator to but doesnt give the correct direction to follow it yet.
			finalEdgePath.Add(nodePath[i].connectingEdges[nodePath[i].bestEdgeIndex]);
			nodePath[i].ResetNode();
		}
		endNode.ResetNode();
		nodePath.Clear();


	}
	/*	void RetracePathToConstructWaypointPath(MyAStarNode startNode, MyAStarNode endNode)
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
	}*/
}
