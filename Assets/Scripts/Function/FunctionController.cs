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
        GameStateController gameStateController = GameStateController.instance;

        if (gameStateController.currStateType == GameStateType.Running)
            FunctionsOn();

        gameStateController.OnGameStart += FunctionsOn;
        gameStateController.OnGamePaused += FunctionsOff;
        gameStateController.OnGameResumed += FunctionsOn;
        gameStateController.OnGameSucceed += FunctionsOff;
        gameStateController.OnGameFailure += FunctionsOff;
    }

    private void OnDeath()
    {
        FunctionsOff();

        GameStateController gameStateController = GameStateController.instance;

        if (gameStateController != null)
        {
            gameStateController.OnGameStart -= FunctionsOn;
            gameStateController.OnGamePaused -= FunctionsOff;
            gameStateController.OnGameResumed -= FunctionsOn;
            gameStateController.OnGameSucceed -= FunctionsOff;
            gameStateController.OnGameFailure -= FunctionsOff;
        }
    }

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