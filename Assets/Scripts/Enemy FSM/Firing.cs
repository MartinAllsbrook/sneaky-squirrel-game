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

    private float _fireCountdown = 1;
    private float _fireTime = 1;
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
        
        var targetLocation = PlayerController2D.Instance.transform.position;
        float angle = Mathf.Atan2(targetLocation.y - _enemyFSM.transform.position.y, targetLocation.x -_enemyFSM.transform.position.x ) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        _enemyFSM.transform.rotation = Quaternion.RotateTowards(_enemyFSM.transform.rotation, targetRotation, _enemyFSM.enemyController.RotateSpeed);

        if (_fireCountdown > 0) _fireCountdown -= Time.deltaTime;
        else
        {
            _enemyFSM.enemyController.Fire();
            _fireCountdown = _fireTime;
        }

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
