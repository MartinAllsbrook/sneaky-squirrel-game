using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Firing : BaseState
{
    private EnemyFSM _enemyFSM;

    public UnityEvent FireEvent;
    private bool _doneFiring = false;
    public bool DoneFiring
    {
        private get { return _doneFiring; }
        set { _doneFiring = value; }
    }

    public Firing(EnemyFSM stateMachine) : base("Firing", stateMachine) {
        _enemyFSM = stateMachine;
        if(FireEvent == null) FireEvent = new UnityEvent();
    }

    public override void Enter()
    {
        _doneFiring = false;
        base.Enter();
        FireEvent.Invoke(); // Call fire event to start firing routine
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic(); // Call original
        if (_doneFiring)
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
