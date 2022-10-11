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
    private float timer;

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
        this.attackTime = attackStateData.attackTime;
    }
    public void OnStateEnter()
    {
        Debug.Log("Enter Attack State");
        timer = 0;
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
        //CheckPlayerDistance();
        AttackToIdleWithTime();
    }

    private void SetStateSpeed()
    {
        navMeshAgent.speed = moveSpeed;
    }

    private void SetAnimatorVariable()
    {
        animator.CrossFade("Attack", 0.2f);
    }

    //private void CheckPlayerDistance()
    //{
    //    if (Vector3.Distance(ownTransform.position, playerTransform.position) > chaseRadius)
    //    {
    //        enemyAI.Idle();
    //    }
    //}

    private void AttackToIdleWithTime()
    {
        timer += Time.deltaTime;
        Debug.Log("T: " + timer + " AT: " + attackTime);
        if (timer > attackTime)
        {
            enemyAI.Idle();
        }
    }
}
