using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateManager), typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Hareket De�i�kenleri")]
    public float speed;

    [Header("Konum De�i�kenleri")]
    public Transform ownTransform;
    public Transform playerTransform;

    [Header("Component De�i�kenleri")]
    public StateManager stateManager;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    [Header("State De�i�kenleri")]
    public EnemyIdleState idleState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;

    [Header("State Veri De�i�kenleri")]
    public IdleStateData idleStateData;
    public ChaseStateData chaseStateData;
    public AttackStateData attackStateData;
    public DeadStateData deadStateData;
    private void Awake()
    {
        ComponentAttach();
        CreateStates();
    }

    private void Start()
    {
        Idle();
    }
    #region Ba�lang�� Fonksiyonlar�
    private void ComponentAttach()
    {
        ownTransform = transform;
        stateManager = GetComponent<StateManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }    
    #endregion


    #region Stateleri Olu�turma ve De�i�tirme

    public void Idle()
    {
        stateManager.ChangeState(idleState);
    }

    public void Chase()
    {
        stateManager.ChangeState(chaseState);
    }

    public void Attack()
    {
        stateManager.ChangeState(attackState);
    }

    public void Dead()
    {
        stateManager.ChangeState(deadState);
    }


    public void CreateStates()
    {
        NewIdleState();
        NewChaseState();
        NewAttackState();
        NewDeadState();
    }

    public void NewIdleState()
    {
        EnemyIdleState newIdleState = new EnemyIdleState(idleStateData, this);
        idleState = newIdleState;
    }
    public void NewChaseState()
    {
        EnemyChaseState newChaseState = new EnemyChaseState(chaseStateData, this);
        chaseState = newChaseState;
    }

    public void NewAttackState()
    {
        EnemyAttackState newAttackState = new EnemyAttackState(attackStateData, this);
        attackState = newAttackState;
    }

    public void NewDeadState()
    {
        EnemyDeadState newDeadState = new EnemyDeadState(deadStateData, this);
        deadState = newDeadState;
    }
    #endregion

    #region Edit�r Kodlar�
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255, 0, 0.5f);
        if (idleStateData)
        {
            Gizmos.DrawWireSphere(transform.position, idleStateData.chaseRadius);
        }

        Gizmos.color = new Color(255, 0, 0, 0.5f);
        if (attackStateData)
        {
            Gizmos.DrawWireSphere(transform.position, attackStateData.attackRadius);
        }
    }
    #endregion
}
