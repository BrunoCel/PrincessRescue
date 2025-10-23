using System;
using UnityEngine;

public class Enemy : Entity
{
  void Awake()
  {
    rigidbody2D= GetComponent<Rigidbody2D>();
    animator = GetComponentInChildren<Animator>();
  }
  

  protected override void HandleAnimations()
  {
    animator.SetFloat("VelocityX", rigidbody2D.linearVelocity.x);
  }
  
  

  protected override void Update()
  {
    HandleCollisions();
    HandleMovement(facingDirection);
    HandleAnimations();
    HandleFlip();
  }

  public override void TakeDamage()
  {
    base.TakeDamage();
  }
}
