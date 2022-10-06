using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    // Hareket
    public float rotateSpeed;

    // Component
    public Animator animator;

    public EnemyAI enemyAI;

    // Konum
    public Transform ownTransform;

    // Algýlama
    public float attackRadius;

    // Saldýrý
    public float attackTime;
    public float nextAttackTime;

    public EnemyAttackState(AttackStateData attackStateData, EnemyAI enemyAI)
    {
        this.rotateSpeed = attackStateData.rotateSpeed;
        this.animator = enemyAI.animator;
        this.enemyAI = enemyAI;
        this.ownTransform = enemyAI.ownTransform;
        this.attackRadius = attackStateData.attackRadius;
        this.attackTime = attackStateData.attackTime;
    }
    public void OnStateEnter()
    {
        Debug.Log("Enter Attack State");
    }

    public void OnStateExit()
    {
        Debug.Log("Exit Attack State");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Update Attack State");
    }
}
