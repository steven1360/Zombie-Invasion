using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    [SerializeField] Tilemap[] tilemap;
    private List<Node> path;
    private int index;
    public Grid grid;

    void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        grid = new Grid(tilemap[0]);
        path = new List<Node>();
        index = 0;
        grid.MarkUnwalkableNodes(tilemap[1]);
    }


    public void ComputeAStarPath(Vector3 start, Vector3 end) 
    {
        path = grid.GetShortestPath(start, end);
        index = 0;
    }

    public bool MoveRigidbodyAlongPath(Rigidbody2D rbToMove, float speed, int stoppingDistanceInGridCoords = 0)
    {
        if (path == null)
        {
            return true;
        }

        bool indexIsValid = index >= 0 && index < (path.Count - stoppingDistanceInGridCoords);
        bool arrivedAtNextNode = indexIsValid && Mathf.Abs(rbToMove.transform.position.x - path[index].worldPosition.x) <= 0.2f &&
                                 Mathf.Abs(rbToMove.transform.position.y - path[index].worldPosition.y) <= 0.2f;

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

    public Vector2 GetRandomWalkablePosition(Vector2 origin)
    {
        float dx;
        float dy;
        Vector2 randomPosition;

        do
        {
            dx = Random.Range(-18f, 18f);
            dy = Random.Range(-18f, 18f);
            while (dx > -1 && dx < 1 && dy > -1 && dy < 1)
            {
                dx = Random.Range(-18f, 18f);
                dy = Random.Range(-18f, 18f);
            }

            randomPosition = new Vector2(origin.x + dx, origin.y + dy);
        } while ( ( grid[randomPosition] == null || !grid[randomPosition].walkable));

        return randomPosition;
    }
}
