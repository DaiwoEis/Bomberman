using System;
using System.Collections;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    [SerializeField]
    private float _initTime = 2f;

    [SerializeField]
    private GameStage _state = GameStage.Init;

    public GameStage state { get { return _state; } }

    public event Action OnGameStart;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event GameOverDelegate OnGameOver;

    private IEnumerator Start()
    {
        GameInit();

        yield return new WaitForSecondsRealtime(_initTime);
        GameStart();
    }

    private void GameInit()
    {
        _state = GameStage.Init;
        Time.timeScale = 1f;
    }

    public void GameStart()
    {
        _state = GameStage.Running;
        if (OnGameStart != null) OnGameStart();
    }

    public void GamePaused()
    {
        Time.timeScale = 0f;
        _state = GameStage.Paused;
        if (OnGamePaused != null) OnGamePaused();
    }

    public void GameResumed()
    {
        Time.timeScale = 1f;
        _state = GameStage.Running;
        if (OnGameResumed != null) OnGameResumed();
    }

    public void GameOver(GameOverType type)
    {
        _state = GameStage.Over;
        if (OnGameOver != null) OnGameOver(type);
    }

    public delegate void GameOverDelegate(GameOverType type);

    public enum GameStage
    {
        Init,
        Running,
        Paused,
        Over
    }

    public enum GameOverType
    {
        Succeed,
        Failure
    }
}
