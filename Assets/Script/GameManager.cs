using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    static public event Action OnGameStarted;
    static public event Action OnGameEnded;

    public enum GameState
    {
        MainMenu,
        InGame,
        GameOver
    }

    public GameState CurrentState;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        OnGameEnded?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
