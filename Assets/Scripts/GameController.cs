using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private UnityEvent _catnipEaten;

    [SerializeField] private GameObject catnipPrefab;

    private Vector3[] _catnipSpawns =
    {
        new Vector3(15f, 5f, 0),
        new Vector3(-5f, 15f, 0),
        new Vector3(-15f, -5f, 0),
        new Vector3(5f, -15f, 0)
    };
        // Start is called before the first frame update
    void Start()
    {
        if (_catnipEaten == null)
        {
            _catnipEaten = PlayerController2D.Instance.CatnipEaten;
            _catnipEaten.AddListener(OnCatnipEaten);
        }
        
    }

    void OnCatnipEaten()
    {
        var spawnNumber = Random.Range(0, 4);
        var location = new Vector3();
        Debug.Log(spawnNumber);
        Instantiate(catnipPrefab, _catnipSpawns[spawnNumber], new Quaternion(0, 0, 0, 0));
    }
}
