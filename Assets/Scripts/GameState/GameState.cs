using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    protected GameStateController _stateController = null;

    private List<StateChangeEvent> _stateChangeEvents = null;

    [SerializeField]
    private GameStateType _stateType;

    public GameStateType stateType { get { return _stateType; } }

    public event Action onEnter = null;

    public event Action onExit = null;

    public GameState(GameStateController stateController, GameStateType stateType)
    {
        _stateController = stateController;
        _stateChangeEvents = new List<StateChangeEvent>();
        _stateType = stateType;
    }

    public void ChangeTo(Func<bool> changeEvent, GameStateType destStateType)
    {
        StateChangeEvent newEvent = new StateChangeEvent
        {
            ifChangeEvent = changeEvent,
            destStateType = destStateType
        };
        _stateChangeEvents.Add(newEvent);
    }

    public virtual void OnEnter()
    {
        if (onEnter != null) onEnter();
    }

    public virtual void OnExit()
    {
        if (onExit != null) onExit();
    }

    public virtual void OnUpdate()
    {
        foreach (StateChangeEvent changeEvent in _stateChangeEvents)
        {
            if (changeEvent.ifChangeEvent())
            {
                _stateController.ChangeState(changeEvent.destStateType);
                return;
            }
        }
    }
}

public enum GameStateType
{
    Init,
    Running,
    Paused,
    Succeed,
    Failure
}

public class GameInit : GameState
{
    public GameInit(GameStateController stateController) : base(stateController, GameStateType.Init)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();

        Time.timeScale = 1f;
    }
}

public class GameRunning : GameState
{
    public GameRunning(GameStateController stateController) : base(stateController, GameStateType.Running)
    {

    }
}

public class GamePaused : GameState
{
    public GamePaused(GameStateController stateController) : base(stateController, GameStateType.Paused)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();

        Time.timeScale = 0f;
    }

    public override void OnExit()
    {
        base.OnExit();

        Time.timeScale = 1f;
    }
}

public class GameFailure : GameState
{
    public GameFailure(GameStateController stateController) : base(stateController, GameStateType.Failure)
    {

    }
}

public class GameSucceed : GameState
{
    public GameSucceed(GameStateController stateController) : base(stateController, GameStateType.Succeed)
    {

    }
}