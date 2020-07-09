using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Grid : MonoBehaviour
{
    private Node[,] arr;
    private Vector2 origin;
    private int cellSize;
    private int width;
    private int height;
    [SerializeField] private Tilemap tilemap;
    List<Node> foo;


    void OnDrawGizmos()
    {
        for (int x = 0; x < arr.GetLength(0); x++)
        {
            for (int y = 0; y < arr.GetLength(1); y++)
            {
                if (arr[x,y].walkable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(arr[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(arr[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }

            }
        }

        foreach(Node node in foo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(node.worldPosition, new Vector3(1, 1, 1) * 0.5f);
        }
    }

    public Node this[int x, int y] {
        get {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return arr[x, y];
            }
            return null;
        }

        set {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                arr[x, y] = value;
            }
        }
    }


    public Node this[Vector3 position] {
        get {
            Vector2Int indices = GetGridCoordinates(position);
            return this[indices.x, indices.y];
        }

        set {
            Vector2Int indices = GetGridCoordinates(position);
            this[indices.x, indices.y] = value;
        }
    }


    void Start()
    {
        cellSize = 1;
        InitGrid();
        MarkUnwalkableNodes();
        //Debug.Log(GetGridCoordinates(new Vector2(2.5f, 2.5f)));
        //Debug.Log(GetGridCoordinates(new Vector2(-4.5f, -9.5f)));
        Testing();
    }

    void Testing()
    {
        foo = GetShortestPath(new Vector2(2.5f, 2.5f), new Vector2(-6.5f, -8.5f));
        Debug.Log("Foo: " + foo);
        foreach (Node node in foo)
        {
            Debug.Log(node.worldPosition);
        }
    }

    public Vector2Int GetGridCoordinates(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt((worldPosition.x - origin.x) / cellSize);
        int y = Mathf.RoundToInt((worldPosition.y - origin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    void InitGrid()
    {
        width = tilemap.cellBounds.size.x;
        height = tilemap.cellBounds.size.y;
        arr = new Node[width, height];
        origin.x = tilemap.origin.x + 0.5f;
        origin.y = tilemap.origin.y + 0.5f;
        arr[0, 0] = new Node(origin);

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

    void MarkUnwalkableNodes()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

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
        List<Node> closedList = new List<Node>();
        Node startNode = this[startPosition];
        Node endNode = this[endPosition];

  

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
            foreach(Node neighbor in neighbors)
            {
                if (closedList.Contains(neighbor)) continue;

                if (!neighbor.walkable)
                {
                    closedList.Add(neighbor);
                    continue;
                }

                int tentativeGCost = currentNode.g + CalculateDistanceCost(currentNode, neighbor);
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

    List<Node> CalculatePath(Node end)
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

    List<Node> GetNeighbors(Vector3 position)
    {
        Vector2Int indices = GetGridCoordinates(position);
        List<Node> neighbors = new List<Node>();

        Node north = this[indices.x, indices.y + 1];
        Node north_east = this[indices.x + 1, indices.y + 1];
        Node north_west = this[indices.x - 1, indices.y + 1];

        Node south = this[indices.x, indices.y - 1];
        Node south_east = this[indices.x + 1, indices.y - 1];
        Node south_west = this[indices.x - 1, indices.y - 1];

        Node west = this[indices.x - 1, indices.y];
        Node east = this[indices.x + 1, indices.y];


        neighbors.Add(north);
        neighbors.Add(north_east);
        neighbors.Add(north_west);

        neighbors.Add(south);
        neighbors.Add(south_east);
        neighbors.Add(south_west);

        neighbors.Add(west);
        neighbors.Add(east);

        neighbors.RemoveAll(item => item == null);


        return neighbors;
    }

    Node NodeWithLowestFScore(List<Node> list)
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

    int CalculateDistanceCost(Node a, Node b)
    {
        int dx = Mathf.Abs(GetGridCoordinates(a.worldPosition).x - GetGridCoordinates(b.worldPosition).x);
        int dy = Mathf.Abs(GetGridCoordinates(a.worldPosition).y - GetGridCoordinates(b.worldPosition).y);
        int remaining = Mathf.Abs(dx - dy);
        return 14 * Mathf.Min(dx, dy) + 10 * remaining;

    }
}