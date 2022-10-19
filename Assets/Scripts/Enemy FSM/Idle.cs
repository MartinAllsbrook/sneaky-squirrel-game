using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private EnemyFSM _enemyFSM;
    
    public Idle(EnemyFSM stateMachine) : base("Idle", stateMachine) {
        _enemyFSM = stateMachine;
    }
    
    public override void UpdateLogic()
    {
        // Debug.Log("idle update");
        base.UpdateLogic();
        MoveOrFire(_enemyFSM);
    }
}
