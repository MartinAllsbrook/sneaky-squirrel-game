using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;
    
    protected bool _notMoveing = false;
    public bool NotMoving
    {
        get { return _notMoveing; }
        set { _notMoveing = value; }
    }

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
        if (NotMoving)
        {
            var vectorToPlayer = PlayerController2D.Instance.transform.position - fsm.transform.position;
            if (Vector3.Angle(vectorToPlayer, fsm.transform.up) < 45)
            {
                RaycastHit2D hit;
                if (hit = Physics2D.Raycast(fsm.transform.position, PlayerController2D.Instance.transform.position - fsm.transform.position, 100f))
                {
                    Debug.Log("hit");
                    if (hit.collider.CompareTag("Cat"))
                    {
                        Debug.Log("cat hit");
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
        }
    }
}