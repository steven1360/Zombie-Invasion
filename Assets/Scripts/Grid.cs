using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Grid 
{
    private Node[,] arr;
    private Tilemap tilemap;
    private int cellSize;

    public Vector2 Origin { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Grid(Tilemap tilemap, int cellSize)
    {
        this.tilemap = tilemap;
        this.cellSize = cellSize;
        InitGrid();
        MarkUnwalkableNodes(tilemap);
    }

    public Node this[int x, int y] {
        get {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return arr[x, y];
            }
            return null;
        }

        set {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                arr[x, y] = value;
            }
        }
    }

    public Node this[Vector3 position] {
        get {
            Vector2Int indices = GetGridIndices(position);
            return this[indices.x, indices.y];
        }

        set {
            Vector2Int indices = GetGridIndices(position);
            this[indices.x, indices.y] = value;
        }
    }

    public void MarkUnwalkableNodes(Tilemap tMap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tMap.GetTilesBlock(bounds); 

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    arr[x, y].walkable = false;
                }

            }
        }
    }

    public List<Node> GetShortestPath(Vector2 startPosition, Vector2 endPosition)
    {
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        Node startNode = this[startPosition];
        Node endNode = this[endPosition];

        for (int x = 0; x < arr.GetLength(0); x++)
        {

            for (int y = 0; y < arr.GetLength(1); y++)
            {
                Node node = arr[x, y];
                node.g = int.MaxValue;
                node.f = node.g + node.h;
                node.parent = null;
            }
        }


        startNode.g = 0;
        startNode.h = CalculateDistanceCost(startNode, endNode);
        startNode.f = startNode.g + startNode.h;

        openList.Add(startNode);
        while (openList.Count > 0)
        {
            Node currentNode = NodeWithLowestFScore(openList);
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            List<Node> neighbors = GetNeighbors(currentNode.worldPosition);
            foreach (Node neighbor in neighbors)
            {
                if (closedList.Contains(neighbor)) continue;

                if (!neighbor.walkable)
                {
                    closedList.Add(neighbor);
                    continue;
                }

                float tentativeGCost = currentNode.g + CalculateDistanceCost(currentNode, neighbor);
                if (tentativeGCost < neighbor.g)
                {
                    neighbor.parent = currentNode;
                    neighbor.g = tentativeGCost;
                    neighbor.h = CalculateDistanceCost(neighbor, endNode);
                    neighbor.f = neighbor.g + neighbor.h;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }



        return null;
    }

    private void InitGrid()
    {
        Width = tilemap.cellBounds.size.x;
        Height = tilemap.cellBounds.size.y;


        arr = new Node[Width, Height];
        // Add the halfsize of a cell so that each node represents the center of a tile rather
        // than its leftmost corner
        Origin = new Vector2((tilemap.origin.x * cellSize) + cellSize/2, (tilemap.origin.y * cellSize) + cellSize/2);
        arr[0, 0] = new Node(Origin);


        //fill bottom-most row
        for (int x = 1; x < arr.GetLength(0); x++)
        {
            float worldPosition_x = arr[x - 1, 0].worldPosition.x + cellSize;
            float worldPosition_y = arr[0, 0].worldPosition.y;
            arr[x, 0] = new Node(new Vector2(worldPosition_x, worldPosition_y));
        }


        //fill left-most column
        for (int y = 1; y < arr.GetLength(1); y++)
        {
            float worldPosition_x = arr[0, 0].worldPosition.x;
            float worldPosition_y = arr[0, y - 1].worldPosition.y + cellSize;
            arr[0, y] = new Node(new Vector2(worldPosition_x, worldPosition_y));
        }

        //fill rest of grid
        for (int x = 1; x < arr.GetLength(0); x++)
        {
            for (int y = 1; y < arr.GetLength(1); y++)
            {
                float worldPosition_x = arr[x, y - 1].worldPosition.x;
                float worldPosition_y = arr[x, y - 1].worldPosition.y + cellSize;
                arr[x, y] = new Node(new Vector2(worldPosition_x, worldPosition_y));
            }
        }
    }

    private Vector2Int GetGridIndices(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt((worldPosition.x - Origin.x) / cellSize);
        int y = Mathf.RoundToInt((worldPosition.y - Origin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    private List<Node> CalculatePath(Node end)
    {
        List<Node> path = new List<Node>();
        path.Add(end);
        Node current = end;
        while (current.parent != null)
        {
            path.Add(current.parent);
            current = current.parent;
        }
        path.Reverse();

        return path;
    }

    private List<Node> GetNeighbors(Vector3 position)
    {
        Vector2Int indices = GetGridIndices(position);
        List<Node> neighbors = new List<Node>();

        Node top = this[indices.x, indices.y + 1];
        Node bottom = this[indices.x, indices.y - 1];
        Node left = this[indices.x - 1, indices.y];
        Node right = this[indices.x + 1, indices.y];

        neighbors.Add(top);
        neighbors.Add(bottom);
        neighbors.Add(left);
        neighbors.Add(right);

        neighbors.RemoveAll(item => item == null);


        return neighbors;
    }

    private Node NodeWithLowestFScore(List<Node> list)
    {
        float smallestF = list[0].f;
        Node lowestFnode = list[0];

        foreach (Node node in list)
        {
            if (node.f < smallestF)
            {
                smallestF = node.f;
                lowestFnode = node;
            }
        }

        return lowestFnode;
    }

    private float CalculateDistanceCost(Node a, Node b)
    {
        //Manhattan distance
        return Mathf.Abs(a.worldPosition.x - b.worldPosition.x) + Mathf.Abs(a.worldPosition.y - b.worldPosition.y);
    }

}