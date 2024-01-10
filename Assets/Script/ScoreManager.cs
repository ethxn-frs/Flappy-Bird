using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;
    public int highScore;

    void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetInt("HighScore");
        else
            highScore = 0;
    }

    public void AddScore()
    {
        score++;

        UIController.Instance.UpdateScore(score);
        GameManager.Instance.speedPipes += 0.15;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
