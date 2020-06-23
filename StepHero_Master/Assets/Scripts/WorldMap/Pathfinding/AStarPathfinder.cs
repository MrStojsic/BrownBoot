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

	public float maxDistancePerGoal = 50f; // UNUSED YET.

	public float totalPathDistance = 0; // UNUSED YET.

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
					{
						node = openSet[i];
					}
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
				otherNode = node.connectingEdges[i].ReturnOtherEndOfPath(node);
				if (closedSet.Contains(otherNode))
				{
					continue;
                }

				float newGScore = node.gScore + node.connectingEdges[i].LScore;
				if (newGScore < otherNode.gScore || !openSet.Contains(otherNode))
				{
					otherNode.gScore = newGScore;
					otherNode.hScore = Vector2.Distance(otherNode.transform.position, destinationNode.transform.position);
					otherNode.previous = node;
					otherNode.bestEdgeIndex = i;

					if (!openSet.Contains(otherNode))
					{
						openSet.Add(otherNode);
					}
				}
			}
		}
		return null;
	}

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
			finalEdgePath.Add(nodePath[i].connectingEdges[nodePath[i].bestEdgeIndex]);
			nodePath[i].ResetNode();
		}
		endNode.ResetNode();
	}
}
