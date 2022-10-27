using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moving : BaseState
{
    private EnemyFSM _enemyFSM;

    public UnityEvent MoveEvent;
    public Moving(EnemyFSM stateMachine) : base("Moving", stateMachine) {
        _enemyFSM = stateMachine;
        if(MoveEvent == null) MoveEvent = new UnityEvent();
    }

    public override void Enter()
    {
        // Debug.Log("entered moving state");
        base.Enter();
        MoveEvent.Invoke();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // Debug.Log(_enemyFSM.NotMoving);
        var distanceToPlayer = _enemyFSM.enemyController.CanSeePlayer();
        if (_enemyFSM.NotMoving && (distanceToPlayer > _enemyFSM.range || distanceToPlayer == 0))
        {
            if (!_enemyFSM.enemyController.AtEndOfPath) stateMachine.ChangeState(_enemyFSM.movingState);
            else if (_enemyFSM.enemyController.AtEndOfPath) stateMachine.ChangeState(_enemyFSM.idleState);
        }
        else MoveOrFire(_enemyFSM);
        // MoveOrFire(_enemyFSM);
    }
}