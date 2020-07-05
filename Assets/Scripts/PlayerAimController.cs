using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    public Vector2 LookDirection { get; private set; }


    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        LookDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;
        LookAtMousePosition(mouseWorldPosition);
    }

    void LookAtMousePosition(Vector2 mouseWorldPosition)
    {
        float dx = mouseWorldPosition.x - transform.position.x;
        float dy = mouseWorldPosition.y - transform.position.y;
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

        transform.parent.eulerAngles = new Vector3(0, 0, angle);
    }

}
