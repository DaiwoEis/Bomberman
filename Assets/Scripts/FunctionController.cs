using UnityEngine;

public class FunctionController : MonoBehaviour
{
    [SerializeField]
    private Function[] _functions = null;

    private void Awake()
    {
        Actor actor = GetComponent<Actor>();
        actor.onSpawn += OnSpawn;
        actor.onDeath += OnDeath;
    }

    private void OnSpawn()
    {
        GameState gameState = Singleton<GameState>.instance;

        if (gameState.state == GameState.GameStage.Running)
            FunctionsOn();

        gameState.OnGameStart += FunctionsOn;
        gameState.OnGamePaused += FunctionsOff;
        gameState.OnGameResumed += FunctionsOn;
        gameState.OnGameOver += OnGameOver;
    }

    private void OnDeath()
    {
        FunctionsOff();

        GameState gameState = Singleton<GameState>.instance;

        if (gameState != null)
        {
            gameState.OnGameStart -= FunctionsOn;
            gameState.OnGamePaused -= FunctionsOff;
            gameState.OnGameResumed -= FunctionsOn;
            gameState.OnGameOver -= OnGameOver;
        }
    }

    private void OnGameOver(GameState.GameOverType type)
    {
        FunctionsOff();
    }

    private void Start() { }

    private void FunctionsOn()
    {
        foreach (Function function in _functions)
        {
            function.On();
        }
    }

    private void FunctionsOff()
    {
        foreach (Function function in _functions)
        {
            function.Off();
        }
    }
}