using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public UnityEvent SpawnEnemy;
    
    [SerializeField] private int scoreToSpawn;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private Image healthBar;
    
    private int _score;
    private int _health = 200;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (SpawnEnemy == null) SpawnEnemy = new UnityEvent();
    }

    public void AddScore()
    {
        _score++;
        if (_score % scoreToSpawn == 0)
        {
            SpawnEnemy.Invoke();
        }
        scoreDisplay.text = "Score: " + _score;
        Debug.Log("Added Score");
    }

    public void ChangeHealth(int deltaHealth)
    {
        var newHealth = _health + deltaHealth;
        if (newHealth >= 0 && newHealth <= 200)
        {
            _health = newHealth;
        }
        var barTransform = healthBar.rectTransform;
        barTransform.sizeDelta = new Vector2 (_health, barTransform.sizeDelta.y);
    }
}
