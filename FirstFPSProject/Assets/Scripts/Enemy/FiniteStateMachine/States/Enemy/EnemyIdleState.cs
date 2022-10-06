using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
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
    public LayerMask soldierLayer;

    private GameObject nearestSoldier;

    public EnemyIdleState(IdleStateData idleStateData, EnemyAI enemyAI)
    {
        this.rotateSpeed = idleStateData.rotateSpeed;
        this.enemyAI = enemyAI;
        this.attackRadius = idleStateData.attackRadius;
    }

    public void OnStateEnter()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Enter Idle State");
    }

    public void OnStateExit()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Exit Idle State");
    }

    public void OnStateUpdate()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Update Idle State");
    }
}
