using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private List<Node> path;
    private int index;
    public Grid grid;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        grid = new Grid(tilemap);
        path = new List<Node>();
        index = 0;
    }

    public delegate void OnTrue();
    public delegate void OnFalse();

    void OnDrawGizmos()
    {
        for (int x = 0; x < grid.Width; x++)
        {

            for (int y = 0; y < grid.Height; y++)
            {
                if (grid[x, y].walkable)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(grid[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(grid[x, y].worldPosition, new Vector3(1, 1, 1) * 0.5f);
                }
            }
        }

        foreach (Node node in path)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(node.worldPosition, new Vector3(1, 1, 1) * 0.5f);
        }
    }

    public void ComputeAStarPath(Vector3 start, Vector3 end) 
    {
        path = grid.GetShortestPath(start, end);
        index = 0;
    }

    public bool MoveRigidbodyAlongPath(Rigidbody2D rbToMove, float speed, int stoppingDistanceInGridCoords = 0)
    {
        bool indexIsValid = index >= 0 && index < (path.Count - stoppingDistanceInGridCoords);
        bool arrivedAtNextNode = indexIsValid && Mathf.Abs(rbToMove.transform.position.x - path[index].worldPosition.x) <= 0.1f &&
                                 Mathf.Abs(rbToMove.transform.position.y - path[index].worldPosition.y) <= 0.1f;

        index = (arrivedAtNextNode) ? index + 1 : index;
        indexIsValid = index >= 0 && index < (path.Count - stoppingDistanceInGridCoords);

        if (!indexIsValid)
        {
            return true;
        }
        else
        {
            Vector2 direction = (path[index].worldPosition - (Vector2)rbToMove.transform.position).normalized;
            LookAt(rbToMove.transform, path[index].worldPosition);
            rbToMove.MovePosition(rbToMove.position + direction * speed * Time.fixedDeltaTime);
            return false;
        }
    }

    void LookAt(Transform source, Vector2 targetToLookAt)
    {
        float dx = targetToLookAt.x - source.position.x;
        float dy = targetToLookAt.y - source.position.y;
        float angle = Mathf.Atan(Mathf.Abs(dy / dx)) * Mathf.Rad2Deg;
        bool mouseInQuadrant2 = dx < 0f && dy > 0f;
        bool mouseInQuadrant3 = dx < 0 && dy < 0;
        bool mouseInQuadrant4 = dx > 0 && dy < 0;

        if (mouseInQuadrant2)
        {
            angle = 90 + (90 - angle);
        }
        else if (mouseInQuadrant3)
        {
            angle += 180;
        }
        else if (mouseInQuadrant4)
        {
            angle = 270 + (90 - angle);
        }

        source.root.eulerAngles = new Vector3(0, 0, angle);
    }

    public Vector2 GetRandomWalkablePosition()
    {
        float dx;
        float dy;
        Vector2 randomPosition;

        do
        {
            dx = Random.Range(-6f, 6f);
            dy = Random.Range(-6f, 6f);
            while (dx >= -1 && dx <= 1)
            {
                dx = Random.Range(-6f, 6f);
            }
            while (dy >= -1 && dy <= 1)
            {
                dy = Random.Range(-6f, 6f);
            }
            randomPosition = new Vector2(transform.position.x + dx, transform.position.y + dy);
        } while (grid[randomPosition] == null || (!grid[randomPosition].walkable));

        return randomPosition;
    }
}
