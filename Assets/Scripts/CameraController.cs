using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform min;
    [SerializeField] private Transform max;
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 velocity;

    private Camera cam;
    private Vector3 min_position;
    private Vector3 max_position;


    void Start()
    {
        cam = GetComponent<Camera>();
        min_position = min.position;
        max_position = max.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraHalfHeight = cam.orthographicSize;
        float cameraHalfWidth = cam.aspect * cameraHalfHeight;
        float leftSidePosition = cam.transform.position.x - cameraHalfWidth;
        float bottomSidePosition = cam.transform.position.y - cameraHalfHeight;
        float rightSidePosition = cam.transform.position.x + cameraHalfWidth;
        float topSidePosition = cam.transform.position.y + cameraHalfHeight;


        if (leftSidePosition <= min_position.x && (player.transform.position.x < transform.position.x))
        {
            transform.position = new Vector3(min_position.x + cameraHalfWidth, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = MoveTowardsAlongX(transform.position, player.transform.position);
        }

        if (rightSidePosition >= max_position.x)
        {
            transform.position = new Vector3(max_position.x - cameraHalfWidth, transform.position.y, transform.position.z);
            if ((player.transform.position.x < transform.position.x))
            {
                transform.position = MoveTowardsAlongX(transform.position, player.transform.position);
            }
        }


        if (bottomSidePosition <= min_position.y)
        {
            transform.position = new Vector3(transform.position.x, min_position.y + cameraHalfHeight, transform.position.z);
            if (player.transform.position.y < transform.position.y)
            {
                transform.position = MoveTowardsAlongY(transform.position, player.transform.position);
            }
        }


        if (topSidePosition >= max_position.y && (player.transform.position.y > transform.position.y))
        {
            transform.position = new Vector3(transform.position.x, max_position.y - cameraHalfHeight, transform.position.z);
        }
        else
        {
            transform.position = MoveTowardsAlongY(transform.position, player.transform.position);
        }



    }

    Vector3 MoveTowardsAlongX(Vector3 current, Vector3 target)
    {
        return Vector3.MoveTowards(current, new Vector3(target.x, current.y, current.z), velocity.x * Time.deltaTime);
    }

    Vector3 MoveTowardsAlongY(Vector3 current, Vector3 target)
    {
        return Vector3.MoveTowards(current, new Vector3(current.x, target.y, current.z), velocity.y * Time.deltaTime);
    }
}