using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void AttackStarted()
    {
        player.EnableMovementAndJump(false);
    }
    
    public void AttackEnded()
    {
        player.EnableMovementAndJump(true);
    }
}
