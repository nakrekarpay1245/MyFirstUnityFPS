using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : IState
{
    // Hareket
    private float moveSpeed;
    private float rotateSpeed;

    // Component
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private EnemyAI enemyAI;

    // Konum
    private Transform ownTransform;
    private Transform playerTransform;

    // Algýlama
    private float chaseRadius;

    // Bekleme
    private float idleWaitTime;
    private float timer;

    public EnemyIdleState(IdleStateData idleStateData, EnemyAI enemyAI)
    {
        this.moveSpeed = idleStateData.moveSpeed;
        this.rotateSpeed = idleStateData.rotateSpeed;
        this.chaseRadius = idleStateData.chaseRadius;
        this.enemyAI = enemyAI;
        this.navMeshAgent = enemyAI.navMeshAgent;
        this.animator = enemyAI.animator;
        this.ownTransform = enemyAI.ownTransform;
        this.playerTransform = enemyAI.playerTransform;
        this.idleWaitTime = idleStateData.waitTime;
    }

    public void OnStateEnter()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Enter Idle State");
        timer = 0;
        SetStateSpeed();
        SetAnimatorVariable();
    }

    public void OnStateExit()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Exit Idle State");
    }

    public void OnStateUpdate()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Update Idle State");
        CheckPlayerDistance();
    }

    private void SetStateSpeed()
    {
        navMeshAgent.speed = moveSpeed;
    }

    private void SetAnimatorVariable()
    {
        animator.SetBool("isWalk", false);
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(ownTransform.position, playerTransform.position) < chaseRadius)
        {
            IdleToChaseWithTime();
        }
    }

    private void IdleToChaseWithTime()
    {
        timer += Time.deltaTime;
        // Debug.Log("T: " + timer + " WT: " + idleWaitTime);
        if (timer > idleWaitTime)
        {
            enemyAI.Chase();
        }
    }
}
