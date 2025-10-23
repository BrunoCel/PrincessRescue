using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
     protected Rigidbody2D rigidbody2D;
     protected Animator animator;
     private float inputX;
     
     
     [SerializeField] protected float moveSpeed = 5f;
     [SerializeField] protected float jumpForce = 10f;
     protected int facingDirection = 1;
     
     [Header("Attack variables")]
     [SerializeField] protected Transform attackPoint;
     [SerializeField] protected float attackRadius;
     [FormerlySerializedAs("whatIsEnemy")] [SerializeField] protected LayerMask whatIsTarget;
     
     protected bool facingRight = true;
     protected bool isGrounded;
     protected bool canJump = true;
     protected bool canMove = true;
     
     [SerializeField] protected float groundDistance = 1.4f;
     [SerializeField] protected LayerMask whatIsGround;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }
    
    protected virtual void Update()
    {
        HandleCollisions();
        HandleInput();
        HandleMovement(inputX);
        HandleAnimations();
        HandleFlip();
        

    }
    protected virtual void HandleMovement(float xDirection)
    {
        if (canMove)
        {
            rigidbody2D.linearVelocity = new Vector2(xDirection * moveSpeed, rigidbody2D.linearVelocity.y);
        }
        else
        {
            rigidbody2D.linearVelocity = new Vector2(0, rigidbody2D.linearVelocity.y);
        }
    }

    protected virtual void HandleAnimations()
    {
        
        
            animator.SetBool("isGrounded",isGrounded);
            animator.SetFloat("yVelocity" , rigidbody2D.linearVelocity.y);
            animator.SetFloat("xVelocity" , rigidbody2D.linearVelocity.x);
        
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

    protected virtual void TryToAttack()
    {
        if (isGrounded)
        {
            
            animator.SetTrigger("attack");
            
        }
    }

    public virtual void DoDamageOnTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider2D collider in enemyColliders)
        {
            
                Entity entity = collider.GetComponent<Entity>();
                entity.TakeDamage();

            
        }
    }

    protected virtual void HandleFlip()
    {
        if (rigidbody2D.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }else if (rigidbody2D.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    protected virtual void HandleCollisions()
    {
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down, groundDistance,whatIsGround);
    }

    protected virtual void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDirection *= -1;
    }
    void TryToJump()
    {
        if (isGrounded && canJump)
        {
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, jumpForce);
        }
    }

    public virtual void TakeDamage()
    {
        Debug.Log("Ai carai!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position + new Vector3(0,groundDistance));
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }
}
