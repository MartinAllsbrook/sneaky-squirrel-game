using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Idle : BaseState
{
    private float idleTime = 0.2f;
    private float idleTimer = 0.2f;
    private EnemyFSM _enemyFSM;
    
    public Idle(EnemyFSM stateMachine) : base("Idle", stateMachine) {
        _enemyFSM = stateMachine;
        // Debug.Log("Entered idle state");
    }
    
    public override void UpdateLogic()
    {
        // Debug.Log("idle update");
        
        if (_enemyFSM.NotMoving)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer < 0f)
            {
                idleTimer = idleTime;
                // Move to random location
                var location = _enemyFSM.transform.position - new Vector3Int(
                    Random.Range(-1, 2),
                    Random.Range(-1, 2),
                    0);
                var x = location.x;
                var y = location.y;
                if (x < 0) x -= 1f;
                if (y < 0) y -= 1f;
                Vector3Int intLocation = new Vector3Int(
                    (int)Math.Truncate(x),
                    (int)Math.Truncate(y),
                    (int)Math.Truncate(location.z)
                );
                if (_enemyFSM.enemyController.enemyAStarManager.WalkableTilemap.HasTile(intLocation))
                {
                    _enemyFSM.enemyController.MoveToLocation(location);
                }

                
                // Debug.Log("Moving to: " + location);

                
            }
        }
        
        base.UpdateLogic();
        MoveOrFire(_enemyFSM);
    }
}
