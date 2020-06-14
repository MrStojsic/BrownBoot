using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AStarEdge : MonoBehaviour
{
    public PathCreator pathCreator;

    public AStarNode headNode = null;
    public AStarNode tailNode = null;

    // The Path ends on the head and tail nodes respectivly so the lenght is purely the paths length.
    public float LScore
    {
        get { return pathCreator.path.length;; }
    }



    // Needs refactoring to use bezeirs.
    public void GetCorrectDirectionPathPoints(AStarNode nextNode)
    {
        bool isHeadNode = (nextNode == headNode);
        // basically if we are the tail node the player will traverse the bezeir curve normally from strt to end 0 > 1.
        // if we are the head node we tell the player to start at the end and reverse at end in  way 1 > 0.
        // ( look in Path follow around line 253 for an idea of what im thinking.)
        // We know with certainty that if the node creation editor works as intended the head node should be the end of the curve, the direction its heading.
        // and the tail node shoould be at the point the curve started.

        // Im not sure what this should return yet so it just returns void for now.

    }

    public AStarNode ReturnOtherEndOfPath(AStarNode end)
    {
        if(end == headNode)
        {
            return tailNode;
        }
        return headNode;
        
    }
}
