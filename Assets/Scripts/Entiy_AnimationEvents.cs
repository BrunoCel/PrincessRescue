using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Entiy_AnimationEvents : MonoBehaviour
{
    [FormerlySerializedAs("player")] [SerializeField] private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DoDamage() => entity.DoDamageOnTargets();

    public void AttackStarted()
    {
        entity.EnableMovementAndJump(false);
    }
    
    public void AttackEnded()
    {
        entity.EnableMovementAndJump(true);
    }
}
