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

        gameStateController.GetState(GameStateType.Running).onEnter += FunctionsOn;
        gameStateController.GetState(GameStateType.Running).onExit += FunctionsOff;

        gameStateController.GetState(GameStateType.Paused).onEnter += FunctionsOff;
        gameStateController.GetState(GameStateType.Paused).onExit += FunctionsOn;

        gameStateController.GetState(GameStateType.Succeed).onEnter += FunctionsOff;
        gameStateController.GetState(GameStateType.Failure).onEnter += FunctionsOff;
    }

    private void OnDeath()
    {
        FunctionsOff();

        GameStateController gameStateController = GameStateController.instance;

        if (gameStateController != null)
        {
            gameStateController.GetState(GameStateType.Running).onEnter -= FunctionsOn;
            gameStateController.GetState(GameStateType.Running).onExit -= FunctionsOff;

            gameStateController.GetState(GameStateType.Paused).onEnter -= FunctionsOff;
            gameStateController.GetState(GameStateType.Paused).onExit -= FunctionsOn;

            gameStateController.GetState(GameStateType.Succeed).onEnter -= FunctionsOff;
            gameStateController.GetState(GameStateType.Failure).onEnter -= FunctionsOff;
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