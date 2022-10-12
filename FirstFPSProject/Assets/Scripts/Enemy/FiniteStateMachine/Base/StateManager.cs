using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public IState currentState;

    public void ChangeState(IState iState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = iState;
        currentState.OnStateEnter();
    }


    public void Update()
    {
        if (currentState != null)
            currentState.OnStateUpdate();
    }
}
