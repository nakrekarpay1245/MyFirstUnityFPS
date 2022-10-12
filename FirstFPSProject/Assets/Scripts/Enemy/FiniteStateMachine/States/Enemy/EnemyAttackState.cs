using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : IState
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
    public float attackRadius;

    // Saldýrý
    private float attackTime;
    private float attackTimer;

    // Bekleme
    private float attackWaitTime;
    private float waitTimer;

    public EnemyAttackState(AttackStateData attackStateData, EnemyAI enemyAI)
    {
        this.rotateSpeed = attackStateData.rotateSpeed;
        this.moveSpeed = attackStateData.moveSpeed;
        this.enemyAI = enemyAI;
        this.animator = enemyAI.animator;
        this.navMeshAgent = enemyAI.navMeshAgent;
        this.ownTransform = enemyAI.ownTransform;
        this.playerTransform = enemyAI.playerTransform;
        this.attackRadius = attackStateData.attackRadius;
        this.chaseRadius = attackStateData.chaseRadius;
        this.attackTime = attackStateData.attackTime;
        this.attackWaitTime = attackStateData.attackWaitTime;
    }

    public void OnStateEnter()
    {
        Debug.Log("Enter Attack State");
        waitTimer = 0;
        attackTimer = attackTime;
        SetStateSpeed();
        SetAnimatorVariable();
    }

    public void OnStateExit()
    {
        Debug.Log("Exit Attack State");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Attack State Update");
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
        if (Vector3.Distance(ownTransform.position, playerTransform.position) > chaseRadius)
        {
            AttackToIdleWithTime();
        }
        else if (Vector3.Distance(ownTransform.position, playerTransform.position) < chaseRadius && Vector3.Distance(ownTransform.position, playerTransform.position) > attackRadius)
        {
            AttackToIdleWithTime();
        }
        else if (Vector3.Distance(ownTransform.position, playerTransform.position) < attackRadius)
        {
            Attack();
            waitTimer = 0;
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime)
        {
            attackTimer = 0;
            animator.CrossFade("Attack", 0.2f);
        }
    }

    private void AttackToIdleWithTime()
    {
        waitTimer += Time.deltaTime;
        // Debug.Log("T: " + waitTimer + " AT: " + attackTime);
        if (waitTimer > attackWaitTime)
        {
            enemyAI.Idle();
        }
    }
}
