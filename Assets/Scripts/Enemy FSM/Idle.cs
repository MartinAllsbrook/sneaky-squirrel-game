using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private EnemyFSM _stateMachine;
    
    public Idle(EnemyFSM stateMachine) : base("Idle", stateMachine) {
        _stateMachine = stateMachine;
    }
}
