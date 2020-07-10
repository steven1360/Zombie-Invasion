using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector2 worldPosition;
    public Node parent;
    public float f, g, h;

    public Node(Vector2 worldPosition, bool walkable = true)
    {
        this.worldPosition = worldPosition;
        this.walkable = walkable;
        g = int.MaxValue;
        f = g + h;
    }
}
