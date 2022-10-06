using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IState
{
    public EnemyAI enemyAI;
    public EnemyDeadState(DeadStateData deadStateData, EnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }
    public void OnStateEnter()
    {
        Debug.Log("Dead State Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Dead State Exit");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Dead State Update");
    }
}