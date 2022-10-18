using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachine
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Moving movingState;

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}