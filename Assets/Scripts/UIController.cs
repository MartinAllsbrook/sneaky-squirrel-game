using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TextMeshProUGUI scoreDisplay;

    private int _score;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
    }

    public void AddScore()
    {
        _score++;
        scoreDisplay.text = "Score: " + _score;
        Debug.Log("Added Score");
    }
}
