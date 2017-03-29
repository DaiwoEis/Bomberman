using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoSingleton<GameStateController>
{
    [SerializeField]
    private GameState _currState = null;

    public GameState currState { get { return _currState; } }

    private Dictionary<GameStateType, GameState> _states = new Dictionary<GameStateType, GameState>();

    protected override void Init()
    {
        base.Init();

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

    public GameState GetState(GameStateType stateType)
    {
        return _states[stateType];
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

    public void ChangeStateEvent(GameStateType currST, GameStateType nextST)
    {
        if (_currState.stateType == currST)
        {
            ChangeState(nextST);
        }
    }
}