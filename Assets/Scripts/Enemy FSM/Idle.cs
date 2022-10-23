using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Idle : BaseState
{
    private float idleTime = 8;
    private float idleTimer = 8;
    private EnemyFSM _enemyFSM;
    
    public Idle(EnemyFSM stateMachine) : base("Idle", stateMachine) {
        _enemyFSM = stateMachine;
        // Debug.Log("Entered idle state");
    }
    
    public override void UpdateLogic()
    {
        // Debug.Log("idle update");
        idleTimer -= Time.deltaTime;
        if (idleTimer < 0f)
        {
            idleTimer = idleTime;
            // Move to random location
            var location = _enemyFSM.transform.position - new Vector3(
                Random.Range(-1, 2),
                Random.Range(-1, 2),
                0f);
            // Debug.Log("Moving to: " + location);

            _enemyFSM.enemyController.MoveToLocation(location);
        }
        base.UpdateLogic();
        MoveOrFire(_enemyFSM);
    }
}
