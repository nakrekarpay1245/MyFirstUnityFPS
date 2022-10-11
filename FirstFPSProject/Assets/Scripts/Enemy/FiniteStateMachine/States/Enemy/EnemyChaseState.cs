using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : IState
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
    private float attackRadius;
    public EnemyChaseState(ChaseStateData chaseStateData, EnemyAI enemyAI)
    {
        this.moveSpeed = chaseStateData.moveSpeed;
        this.rotateSpeed = chaseStateData.rotateSpeed;
        this.chaseRadius = chaseStateData.chaseRadius;
        this.attackRadius = chaseStateData.attackRadius;
        this.enemyAI = enemyAI;
        this.navMeshAgent = enemyAI.navMeshAgent;
        this.animator = enemyAI.animator;
        this.ownTransform = enemyAI.ownTransform;
        this.playerTransform = enemyAI.playerTransform;
    }

    public void OnStateEnter()
    {
        Debug.Log("Chase State Enter");
        SetStateSpeed();
        SetAnimatorVariable();
    }

    public void OnStateExit()
    {
        Debug.Log("Chase State Exit");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Chase State Update");
        CheckPlayerDistance();
        ChasePlayer();
    }

    private void SetStateSpeed()
    {
        navMeshAgent.speed = moveSpeed;
    }

    private void SetAnimatorVariable()
    {
        animator.SetBool("isWalk", true);
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(ownTransform.position, playerTransform.position) < attackRadius)
        {
            enemyAI.Attack();
        }
        else if (Vector3.Distance(ownTransform.position, playerTransform.position) > chaseRadius)
        {
            enemyAI.Idle();
        }
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);
    }
}
