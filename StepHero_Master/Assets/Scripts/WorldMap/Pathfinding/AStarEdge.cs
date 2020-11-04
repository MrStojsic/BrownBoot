using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AStarEdge : MonoBehaviour
{
    public PathCreator pathCreator;
    public RandomPathEvent randomPathEvent;

    public AStarNode headNode = null;
    public AStarNode tailNode = null;

    public bool isTraversable = true;

    public float inspectorLenght = 0;


    // HACK.
    private void Start()
    {
        inspectorLenght = pathCreator.path.length;
    }// TOHERE.

    // The Path ends on the head and tail nodes respectivly so the lenght is purely the paths length.
    public float LScore
    {
        get { return pathCreator.path.length; }
    }

    /*
    private void OnValidate()
    {
        transform.name = "Path - " + headNode.name + " > " + tailNode.name;
    }*/


    // Needs refactoring to use bezeirs.
    public bool GetDirectionOfPath(AStarNode nextNode)
    {
        // basically if we are the tail node the player will traverse the bezeir curve normally from strt to end 0 > 1.
        // if we are the head node we tell the player to start at the end and reverse at end in  way 1 > 0.
        // ( look in Path follow around line 253 for an idea of what im thinking.)
        // We know with certainty that if the node creation editor works as intended the head node should be the end of the curve, the direction its heading.
        // and the tail node shoould be at the point the curve started.
        return (nextNode == headNode);
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
