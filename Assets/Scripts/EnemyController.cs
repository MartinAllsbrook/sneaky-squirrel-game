using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask canSee;
    [SerializeField] private AStarManager enemyAStarManager;
    [SerializeField] private EnemyFSM enemyFSM;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    
    private UnityEvent _fireEvent;

    private UnityEvent _moveEvent;
    // Start is called before the first frame update
    void Start()
    {
        if (_fireEvent == null) _fireEvent = enemyFSM.firingState.FireEvent;
        _fireEvent.AddListener(OnFireEvent);

        if (_moveEvent == null) _moveEvent = enemyFSM.movingState.MoveEvent;
        _moveEvent.AddListener(OnMoveEvent);
    }

    // Update is called once per frame
    void Update()
    {
        /*var vectorToPlayer = PlayerController2D.Instance.transform.position - transform.position;
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
        }*/
    }

    void OnFireEvent()
    {
        StartCoroutine(FireAtPlayer());
    }
    
    IEnumerator FireAtPlayer()
    {
        // Warm up & turn
        // Debug.Log("Entered Fire");
        yield return new WaitForSeconds(0.25f);
        
        // Fire
        // Debug.Log("Fire!");
        // Instantiate(new )
        yield return new WaitForSeconds(0.25f);
        
        // Cool Down
        enemyFSM.firingState.DoneFiring = true;
        // Debug.Log("Fire Coroutine over");
    }


    void OnMoveEvent()
    {
        StartCoroutine(MoveOneUnit());
    }

    IEnumerator MoveOneUnit()
    {   
        // Rotate
        var location = enemyAStarManager.GetNextLocation();
        Debug.Log("Moving to: " + location);
        float angle = Mathf.Atan2(location.y - transform.position.y, location.x -transform.position.x ) * Mathf.Rad2Deg - 90;
        Debug.Log(angle);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // var q = Quaternion.LookRotation(location - transform.position);
        while (targetRotation.eulerAngles != transform.rotation.eulerAngles)
        {
            // Debug.Log("Rotating");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        while (location != transform.position)
        {
            Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(transform.position, location, moveSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        // Move
        enemyFSM.movingState.DoneMoving = true;
    }
}
