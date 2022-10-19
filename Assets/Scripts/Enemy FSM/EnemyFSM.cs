using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachine
{
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Moving movingState;
    [HideInInspector] public Firing firingState;

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        firingState = new Firing(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}