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
        base.Enter();
        _notMoveing = false;
        MoveEvent.Invoke();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_notMoveing)
        {
            var vectorToPlayer = PlayerController2D.Instance.transform.position - _enemyFSM.transform.position;
            if (Vector3.Angle(vectorToPlayer, _enemyFSM.transform.up) < 45)
            {
                if (vectorToPlayer.magnitude < _enemyFSM.range) stateMachine.ChangeState(_enemyFSM.firingState);
                else stateMachine.ChangeState(_enemyFSM.movingState);
            }
            else
            {
                stateMachine.ChangeState(_enemyFSM.idleState);
            }
        }
    }
}