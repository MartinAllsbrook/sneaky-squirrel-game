using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private EnemyFSM _stateMachine;
    public Moving(EnemyFSM stateMachine) : base("Idle", stateMachine) {
        _stateMachine = stateMachine;
    }
}