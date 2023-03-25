using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAStarNode : MonoBehaviour
{
    public Vector2 position;

    public BasicAStarNode previous;

    public float gScore;
    public float hScore;
    public float fScore { get { return gScore + hScore; } }

    public List<BasicAStarNode> connectingNodes = new List<BasicAStarNode>();

    public void ResetNode()
    {
        this.previous = null;
        this.gScore = 99999;
    }

    private void Awake()
    {
        position = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (connectingNodes == null)
        {
            return;
        }

        Gizmos.color = new Color(0f, 0f, 0f);
        foreach (var connection in connectingNodes)
        {
            if (connection != null)
            {
                Gizmos.DrawLine(transform.position, connection.transform.position);
            }
        }
    }
}
