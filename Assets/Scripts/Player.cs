using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     private Rigidbody2D rigidbody2D;
     private Animator animator;
     private float inputX;
     
     [SerializeField] float moveSpeed = 5f;
     [SerializeField] float jumpForce = 10f;
     
     private bool facingRight = true;
     private bool isGrounded;
     [SerializeField] float groundDistance = 1.4f;
     [SerializeField] LayerMask whatIsGround;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        HandleCollisions();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
        

    }
    void HandleMovement()
    {
        rigidbody2D.velocity = new Vector2(inputX * moveSpeed, rigidbody2D.velocity.y);
    }

    private void HandleAnimations()
    {
        
        
            animator.SetBool("isGrounded",isGrounded);
            animator.SetFloat("yVelocity" , rigidbody2D.velocity.y);
            animator.SetFloat("xVelocity" , rigidbody2D.velocity.x);
        
    }

    void HandleInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryToJump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryToAttack();
        }
    }

    private void TryToAttack()
    {
        animator.SetTrigger("attack");
    }

    void HandleFlip()
    {
        if (rigidbody2D.velocity.x > 0 && !facingRight)
        {
            Flip();
        }else if (rigidbody2D.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void HandleCollisions()
    {
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down, groundDistance,whatIsGround);
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
    void TryToJump()
    {
        if (isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(0,groundDistance));
    }
}
