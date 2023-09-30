using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public UnityEvent SpawnEnemy;
    
    [SerializeField] private int scoreToSpawn;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject gameOverScreen;
    
    private int _score;
    private int _health = 200;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (SpawnEnemy == null) SpawnEnemy = new UnityEvent();
        gameOverScreen.SetActive(false);
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
        else if (newHealth < 0)
        {
            _health = 0;
        }
        var barTransform = healthBar.rectTransform;
        barTransform.sizeDelta = new Vector2 (_health, barTransform.sizeDelta.y);
        if (_health <= 0 )
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
