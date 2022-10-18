using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    [SerializeField] private LayerMask canSee;
    [SerializeField] private AStarManager enemyAStarManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vectorToPlayer = PlayerController2D.Instance.transform.position - transform.position;
        if (Vector3.Angle(vectorToPlayer, transform.up) < 45)
        {
            // Debug.Log("player in range");
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, PlayerController2D.Instance.transform.position - transform.position, 100f, canSee))
            {
                if (hit.collider.CompareTag("Cat"))
                {
                    enemyAStarManager.MoveToPlayer();
                }
                else
                {
                    // eagleFSM.SetBool("CanSeePlayer", false);
                }
            }
        }
    }
}
