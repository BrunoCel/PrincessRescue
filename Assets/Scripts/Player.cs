using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
     private float inputX;
     [SerializeField] float moveSpeed = 5f;
     [SerializeField] float jumpForce = 10f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
        HandleInput();
        HandleMovement();
        
    }

    void HandleInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
    }

    void HandleMovement()
    {
        rigidbody2D.velocity = new Vector2(inputX * moveSpeed, rigidbody2D.velocity.y);
    }
}
