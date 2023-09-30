using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject catnipPrefab;
    [SerializeField] private GameObject enemyPrefab;
    
    private UnityEvent _catnipEaten;
    private UnityEvent _spawnEnemy;
    
    private Vector3[] _catnipSpawns =
    {
        new Vector3(15f, 5f, 0),
        new Vector3(-5f, 15f, 0),
        new Vector3(-15f, -5f, 0),
        new Vector3(5f, -15f, 0),
        new Vector3(12, -6, 0),
        new Vector3(6, 12, 0),
        new Vector3(-12, 6, 0),
        new Vector3(-6, -12, 0),


    };

    private Vector3[] _enemySpawns =
    {
        new Vector3(15.5f, 15.5f, 0),
        new Vector3(15.5f, -15.5f, 0),
        new Vector3(-15.5f, -15.5f, 0),
        new Vector3(-15.5f, 15.5f, 0)
    };
        // Start is called before the first frame update
    void Start()
    {
        if (_catnipEaten == null)
        {
            _catnipEaten = PlayerController2D.Instance.CatnipEaten;
            _catnipEaten.AddListener(OnCatnipEaten);
        }

        if (_spawnEnemy == null)
        {
            _spawnEnemy = UIController.Instance.SpawnEnemy;
            _spawnEnemy.AddListener(OnSpawnEnemy);
        }
    }

    void OnCatnipEaten()
    {
        var spawnNumber = Random.Range(0, 8);
        Instantiate(catnipPrefab, _catnipSpawns[spawnNumber], new Quaternion(0,0,0,0));
    }

    void OnSpawnEnemy()
    {
        Debug.Log("SpawnEnemy");
        var spawnNumber = Random.Range(0, _enemySpawns.Length);
        Instantiate(enemyPrefab, _enemySpawns[spawnNumber], new Quaternion(0, 0, 0, 0));
    }
}
