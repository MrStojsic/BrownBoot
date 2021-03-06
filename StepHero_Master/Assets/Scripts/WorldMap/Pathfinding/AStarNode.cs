﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : MonoBehaviour
{

    public AStarNode previous;

    public int bestEdgeIndex = 0;
    public int previousBestEdgeIndex = 0;

    [SerializeField] private Location _location = default;
    public Location Location
    {
        get { return _location; }
    }

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
