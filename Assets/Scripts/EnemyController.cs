using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public AStarManager enemyAStarManager;
    [SerializeField] private EnemyFSM enemyFSM;
    [SerializeField] public LayerMask canSee;

    [SerializeField] private bool ReachedEndOfPath;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private GameObject missilePrefab;
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        private set {}
    }
    [SerializeField] private float moveSpeed;
    
    private UnityEvent _fireEvent;
    private UnityEvent _moveEvent;

    private List<Spot> _currentPath;
    private List<Spot> _lastPath;
    private int _pathLocation = 1;
    public bool AtEndOfPath = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_fireEvent == null) _fireEvent = enemyFSM.firingState.FireEvent;
        _fireEvent.AddListener(OnFireEvent);

        if (_moveEvent == null) _moveEvent = enemyFSM.movingState.MoveEvent;
        _moveEvent.AddListener(OnMoveEvent);
    }

    void OnFireEvent()
    {
        StartCoroutine(FireAtPlayer());
    }
    
    IEnumerator FireAtPlayer()
    {
        enemyFSM.NotMoving = false;
        // Warm up & turn
        // Debug.Log("Entered Fire");
        yield return new WaitForSeconds(0.25f);
        
        // Fire
        // Debug.Log("Fire!");
        // Instantiate(new )
        yield return new WaitForSeconds(0.25f);
        
        // Cool Down
        enemyFSM.firingState.DoneFiring = true;
        enemyFSM.NotMoving = true;
        // Debug.Log("Fire Coroutine over");
    }

    public float CanSeePlayer()
    {
        // Debug.Log("Not Moving");
        var vectorToPlayer = PlayerController2D.Instance.transform.position - transform.position;
        if (Vector3.Angle(vectorToPlayer, transform.up) < 45)
        {
            // Debug.Log("player In range");
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(transform.position, PlayerController2D.Instance.transform.position - transform.position, 100f, canSee))
            {
                if (hit.collider.CompareTag("Cat"))
                {
                    return vectorToPlayer.magnitude;
                }
                return 0f;
            }
            return 0f;
        }
        return 0f;
    }


    void OnMoveEvent()
    {
        if (CanSeePlayer() > 0f)
        {
            _currentPath = enemyAStarManager.GetPath(); // If we can see the player => Make a new path to the player
        }
        
        if (_lastPath == null)
        {
            _lastPath = _currentPath;
        }
        else
        {
            if (_currentPath[_currentPath.Count - 1] == _lastPath[_lastPath.Count - 1])
            {
                _pathLocation++;
            }
            else
            {
                _pathLocation = 1;
                _lastPath = _currentPath;
            }
        }
        
        Debug.Log(_pathLocation);
        if (_pathLocation == _lastPath.Count - 1) AtEndOfPath = true;
        else AtEndOfPath = false;
        var firstStep = _lastPath[_pathLocation];
        var firstStepLocation = new Vector3(firstStep.X + 0.5f, firstStep.Y + 0.5f, 0);
        StartCoroutine(MoveOneUnit(firstStepLocation));
    }

    public void MoveToLocation(Vector3 location)
    {
        StartCoroutine(MoveOneUnit(location));
    }

    IEnumerator MoveOneUnit(Vector3 location)
    {   
        enemyFSM.NotMoving = false;
        // Rotate
        // Debug.Log("Moving to: " + location);
        float angle = Mathf.Atan2(location.y - transform.position.y, location.x -transform.position.x ) * Mathf.Rad2Deg - 90;
        // Debug.Log(angle);
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
            // Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(transform.position, location, moveSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        // Move
        enemyFSM.NotMoving = true;
    }

    public void Fire()
    {
        Debug.Log("fire");
        Instantiate(missilePrefab, transform.position + transform.up, transform.rotation);
    }
}
