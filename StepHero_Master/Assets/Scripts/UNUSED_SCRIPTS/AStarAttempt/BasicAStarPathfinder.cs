using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//- This AStar code and node system is based off SebLague Lagues A* found at.
// https://github.com/SebLague/Pathfinding/tree/master/Episode%2003%20-%20astar/Assets/Scripts
// - For references to FindClosestWaypoint for when player is selecting waypoints to move to try.
// http://www.trickyfast.com/2017/09/21/building-a-waypoint-pathing-system-in-unity/

public class BasicAStarPathfinder : MonoBehaviour
{
	public BasicAStarNode start, goal;
	List<BasicAStarNode> path = new List<BasicAStarNode>();


	void Start()
	{
		FindPath(start, goal);
	}

	void FindPath(BasicAStarNode start, BasicAStarNode target)
	{

		List<BasicAStarNode> openSet = new List<BasicAStarNode>();
		HashSet<BasicAStarNode> closedSet = new HashSet<BasicAStarNode>();
		openSet.Add(start);

		while (openSet.Count > 0)
		{
			BasicAStarNode node = openSet[0];
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
				RetracePath(start, target);
				return;
			}

			foreach (BasicAStarNode neighbour in node.connectingNodes)
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
		}
	}

	void RetracePath(BasicAStarNode startNode, BasicAStarNode endNode)
	{
	
		BasicAStarNode currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.previous;
		}
		path.Reverse();
        foreach (var item in path)
        {
            print(item.gameObject.name);
        }

	}
}
