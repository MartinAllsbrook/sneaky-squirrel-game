﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarManager : MonoBehaviour
{
    private Tilemap walkableTilemap;
    [SerializeField] private float moveTime;
    [SerializeField] private float numMoves;
    [SerializeField] private float baseStepAngle;
    //Note: In C#, variables without an access modifier are private by default
    Vector3Int[,] walkableArea;
    Astar astar;
    BoundsInt bounds;
    private Rigidbody2D _enemyRigidbody2D;
    private Vector2Int _currentTarget;

    public Tilemap WalkableTilemap
    {
        get { return walkableTilemap; }
        private set {}
    }

    private Vector3Int GridPositionOfPlayer3D
    {
        get { return walkableTilemap.WorldToCell(PlayerController2D.Instance.transform.position); }
    }
    private Vector2Int GridPositionOfPlayer2D => (Vector2Int)GridPositionOfPlayer3D;

    private Vector2Int GridPositionOfCharacter
    {
        get
        {
            return (Vector2Int)walkableTilemap.WorldToCell(transform.position);
        }
    }
    
    private void Start()
    {
        if (_enemyRigidbody2D == null) _enemyRigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        walkableTilemap = GameObject.FindWithTag("Walkable Tilemap").GetComponent<Tilemap>();
        //Trims any empty cells from the edges of the tilemap
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;

        CreateGrid();
        astar = new Astar(walkableArea, bounds.size.x, bounds.size.y);
    }

    private void CreateGrid()
    {
        walkableArea = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (walkableTilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    public List<Spot> GetPath()
    {
        return astar.CreatePath(walkableArea, GridPositionOfCharacter, GridPositionOfPlayer2D);
    }

    public Vector3 GetNextLocation()
    {
        var path = astar.CreatePath(walkableArea, GridPositionOfCharacter, GridPositionOfPlayer2D);
        var firstStep = path[1];
        return new Vector3(firstStep.X + 0.5f, firstStep.Y + 0.5f, 0);
    }

    public void MoveToPlayer()
    {
        // Debug.Log("Moving to player");
        var path = astar.CreatePath(walkableArea, GridPositionOfCharacter, GridPositionOfPlayer2D);
        Debug.Log(GridPositionOfPlayer2D != _currentTarget);
        if (path != null && (GridPositionOfPlayer2D != _currentTarget || _currentTarget == null))
        {
            StopAllCoroutines();
            _currentTarget = GridPositionOfPlayer2D;
            StartCoroutine(MoveToTarget(path));
        }
    }

    IEnumerator MoveToTarget(List<Spot> path)
    {
        foreach (var step in path)
        {
            var target = new Vector3(step.X + 0.5f, step.Y + 0.5f, 0f);
            var deltaTarget = target - transform.position;
            
            // Set tank angle
            var targetDirection = deltaTarget.normalized;
            var targetAngle = unitToSquare(targetDirection);
            
            var directionDifference = targetDirection - transform.up;
            // if (Vector3.Project(targetDirection, transform.up).magnitude > 0.9)
            // else if (Vector3.Project(targetDirection, transform.right).magnitude > 0.9)
            
            // Rotate
            var degrees = degreesToRotate(targetDirection);
            var finalRotation = transform.rotation.eulerAngles.z - degrees;
            var stepAngle = baseStepAngle;
            if (degrees > 0)
            {
                stepAngle *= -1;
            }
            var stepCount = -degrees / stepAngle;
            for (var i = 0; i < stepCount; i++)
            {
                _enemyRigidbody2D.MoveRotation(transform.rotation.eulerAngles.z + stepAngle);
                yield return new WaitForSeconds(moveTime/stepCount);
            }
            _enemyRigidbody2D.MoveRotation(finalRotation);

            // Move
            var deltaMove = deltaTarget / numMoves;
            for(var i = 0; i < numMoves; i++)
            {
                _enemyRigidbody2D.MovePosition(transform.position + deltaMove);
                // deltaTarget = target - transform.position;
                // Debug.Log(deltaTarget.magnitude);
                yield return new WaitForSeconds(moveTime/numMoves);
            }
            _enemyRigidbody2D.MovePosition(target);
        }
        yield return null;
    }

    int unitToSquare(Vector3 direction)
    {
        var angle = 0;
        if (direction.x > 0.9) angle = -90;
        else if (direction.x < -0.9) angle = 90;
        else if (direction.y > 0.9) angle = 0;
        else if (direction.y < -0.9) angle = 180;
        return angle;
    }

    int degreesToRotate(Vector3 direction)
    {
        var angle = 0;
        if ((direction - transform.up).magnitude < 0.1) angle = 0;
        else if ((direction - transform.up).magnitude > 1.9) angle = 180;
        else if ((direction - transform.right).magnitude < 0.1) angle = 90;
        else if ((direction - transform.right).magnitude > 1.9) angle = -90;
        // else Debug.Log("ERROR SETTING TANK AGNLE");
        return angle;
    }
}
