using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : StateMachine
{
    [SerializeField] public EnemyController enemyController;
    [SerializeField] public LayerMask canSee;

    [HideInInspector] public Idle idleState;
    [HideInInspector] public Moving movingState;
    [HideInInspector] public Firing firingState;
    
    private bool _notMoveing = true;
    public bool NotMoving
    {
        get { return _notMoveing; }
        set { _notMoveing = value; }
    }

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