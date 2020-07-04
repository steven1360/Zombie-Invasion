﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5f;


    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }


    void Update()
    {
        UpdatePlayerPosition();
    }


    void UpdatePlayerPosition()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 destination = Vector2.zero;

        destination += (Vector2.right * horizontal * speed * Time.deltaTime);
        destination += (Vector2.up * vertical * speed * Time.deltaTime);
        //rb.MovePosition((Vector2)transform.parent.position + destination);
        transform.parent.position += (Vector3)destination;
    }

}