using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public Grid Grid { get; private set; }
    public int CurrentPathIndex;
    List<Node> path;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new Grid(tilemap);
        path = new List<Node>();
    }

    void OnDrawGizmos()
    {
        for (int x = 0; x < Grid.Width; x++)
        {

            for (int y = 0; y < Grid.Height; y++)
            {
                if (Grid[x, y].walkable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(Grid[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(Grid[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }
            }
        }

        foreach (Node node in path)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(node.worldPosition, new Vector3(1, 1, 1) * 0.5f);
        }
    }

   public List<Node> AStarPath(Vector3 start, Vector3 end) 
   {
        path = Grid.GetShortestPath(start, end);
        return path;
   }

}
