using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IState
{
    public EnemyAI enemyAI;
    public EnemyChaseState(ChaseStateData chaseStateData, EnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }

    public void OnStateEnter()
    {
        Debug.Log("Chase State Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Chase State Exit");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Chase State Update");
    }
}
