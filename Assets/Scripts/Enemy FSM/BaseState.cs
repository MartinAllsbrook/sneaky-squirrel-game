using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }

    protected void MoveOrFire(EnemyFSM fsm)
    {
        var vectorToPlayer = PlayerController2D.Instance.transform.position - fsm.transform.position;
        if (Vector3.Angle(vectorToPlayer, fsm.transform.up) < 45)
        {
            if (vectorToPlayer.magnitude < fsm.range)
            {
                stateMachine.ChangeState(fsm.firingState);
            }
            else
            {
                stateMachine.ChangeState(fsm.movingState);
            }
        }
    }
}