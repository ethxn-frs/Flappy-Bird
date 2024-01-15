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

    public double speedPipes;
    public float numberPipes;
    public float distanceBetweenPipes;
    public Pipe pipePrefab;

    public Transform pipeSpawnPoint;

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

    private void Start()
    {
        CurrentState = GameState.MainMenu;

        for ( int i = 0; i < numberPipes; i++)
        {
            Pipe pipe = Instantiate(pipePrefab, pipeSpawnPoint.position + new Vector3(i * distanceBetweenPipes, 0, 0), Quaternion.identity);
        }
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        CameraController.Instance.Shake(0.3f, 0.25f);
        AudioManager.Instance.PLaySound(AudioType.Hit, AudioSourceType.Player);
        AudioManager.Instance.PLaySound(AudioType.Die, AudioSourceType.Game);
        OnGameEnded?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void OpenSelectPlayerMenu()
    {
        UIController.Instance.OpenSelectPlayerMenu();
    }

    public void CloseSelectPlayerMenu()
    {
        UIController.Instance.CloseSelectPlayerMenu();
    }
}
