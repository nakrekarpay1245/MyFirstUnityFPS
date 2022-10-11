using UnityEngine;
using UnityEngine.AI;

public class EnemyDeadState : IState
{
    // Hareket
    private float moveSpeed;

    // Component
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private EnemyAI enemyAI;
    public EnemyDeadState(DeadStateData deadStateData, EnemyAI enemyAI)
    {
        this.moveSpeed = deadStateData.moveSpeed;
        this.enemyAI = enemyAI;
        this.navMeshAgent = enemyAI.navMeshAgent;
        this.animator = enemyAI.animator;
    }

    public void OnStateEnter()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Enter Dead State");
        SetStateSpeed();
        SetAnimatorVariable();
    }


    public void OnStateExit()
    {
        Debug.Log("Dead State Exit");
    }

    public void OnStateUpdate()
    {
        Debug.Log(enemyAI.gameObject.name + " Enemy Update Dead State");
    }

    private void SetStateSpeed()
    {
        navMeshAgent.speed = moveSpeed;
    }

    private void SetAnimatorVariable()
    {
        animator.SetBool("isWalk", false);
        animator.CrossFade("Dead", 0.2f);
    }
}