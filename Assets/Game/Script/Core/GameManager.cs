using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;
    void Start()
    {
        StartCoroutine(DelayChangeGameState(GameState.LoadLevel, 0.15f));
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.LoadLevel:
                OnGameStateLoadLevel();
                break;
            case GameState.Playing:
                OnGameStatePlaying();
                break;
            case GameState.ResultPhase:
                OnGameStateResultPhase();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }
    public enum GameState
    {
        LoadLevel,
        Playing,
        ResultPhase
    }
    private void OnGameStateLoadLevel()
    {
        Debug.Log("GameStateLoadLevel");
    }
    private void OnGameStatePlaying()
    {
        Debug.Log("GameStatePlaying");
    }
    private void OnGameStateResultPhase()
    {
        Debug.Log("GameStateResultPhase");
    }
    IEnumerator DelayChangeGameState(GameState state, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeGameState(state);
    }
}
