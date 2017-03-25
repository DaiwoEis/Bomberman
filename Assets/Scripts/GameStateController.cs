using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoSingleton<GameStateController>
{
    [SerializeField]
    private GameState _currState = null;

    private Dictionary<GameStateType, GameState> _states = new Dictionary<GameStateType, GameState>();

    private void Awake()
    {
        _states[GameStateType.Init] = new GameInit(this);
        _states[GameStateType.Running] = new GameRunning(this);
        _states[GameStateType.Paused] = new GamePaused(this);
        _states[GameStateType.Succeed] = new GameSucceed(this);
        _states[GameStateType.Failure] = new GameFailure(this);

        ChangeState(GameStateType.Init);
    }

    private void Update()
    {
        if (_currState != null)
        {
            _currState.OnUpdate();
        }
    }

    public GameStateType currStateType { get { return _currState.stateType; } }

    public event Action OnGameStart;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameSucceed;
    public event Action OnGameFailure;

    public void GameInit()
    {
        Time.timeScale = 1f;
    }

    public void GameStart()
    {
        if (OnGameStart != null) OnGameStart();
    }

    public void GamePaused()
    {
        Time.timeScale = 0f;
        if (OnGamePaused != null) OnGamePaused();
    }

    public void GameResumed()
    {
        Time.timeScale = 1f;
        if (OnGameResumed != null) OnGameResumed();
    }

    public void GameSucceed()
    {
        if (OnGameSucceed != null) OnGameSucceed();
    }

    public void GameFailure()
    {
        if (OnGameFailure != null) OnGameFailure();
    }

    public void ChangeState(GameStateType nextStateType)
    {
        if (_currState != null)
        {
            _currState.OnExit();            
        }

        _currState = _states[nextStateType];

        if (_currState != null)
        {
            _currState.OnEnter();
        }
    }

    public GameState When(GameStateType stateType)
    {
        return _states[stateType];
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

[Serializable]
public class GameState
{
    protected GameStateController _stateController = null;

    private List<StateChangeEvent> _stateChangeEvents = null;

    [SerializeField]
    private GameStateType _stateType;

    public GameStateType stateType { get { return _stateType; } }

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
            changeEvent = changeEvent,
            destStateType = destStateType
        };
        _stateChangeEvents.Add(newEvent);
    }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }

    public virtual void OnUpdate()
    {
        foreach (StateChangeEvent changeEvent in _stateChangeEvents)
        {
            if (changeEvent.changeEvent())
            {
                _stateController.ChangeState(changeEvent.destStateType);
                return;
            }
        }
    }

    public class StateChangeEvent
    {
        public Func<bool> changeEvent { get; set; }

        public GameStateType destStateType { get; set; }
    }
}

public class GameInit : GameState
{
    public GameInit(GameStateController stateController) : base(stateController, GameStateType.Init)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();

        _stateController.GameInit();
    }


    public override void OnExit()
    {
        base.OnExit();

        _stateController.GameStart();
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

        _stateController.GamePaused();
    }

    public override void OnExit()
    {
        base.OnExit();

        _stateController.GameResumed();
    }
}

public class GameFailure : GameState
{
    public GameFailure(GameStateController stateController) : base(stateController, GameStateType.Failure)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();

        _stateController.GameFailure();
    }

    public override void OnExit()
    {
        base.OnExit();

        _stateController.GameStart();
    }
}

public class GameSucceed : GameState
{
    public GameSucceed(GameStateController stateController) : base(stateController, GameStateType.Succeed)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();

        _stateController.GameSucceed();
    }
}