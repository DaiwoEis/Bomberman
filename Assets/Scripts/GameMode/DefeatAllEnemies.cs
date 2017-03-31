using CUI;
using UnityEngine;

public class DefeatAllEnemies : GameMode
{
    [SerializeField]
    private int _aliveEnemyCount = 0;

    public int aliveEnemyCount { get { return _aliveEnemyCount; } set { _aliveEnemyCount = value; } }

    [SerializeField]
    private bool _gameStart = false;

    public bool playerEnterTheDoor { get; set; }

    private Door _door = null;

    [SerializeField]
    private float _initTime = 4f;

    [SerializeField]
    private BaseView _pausedView = null;

    [SerializeField]
    private PlayerLifeController _playerLifeController = null;

    private void Start()
    {
        _door = GameObject.FindWithTag(TagConfig.DOOR).GetComponent<Door>();

        InitGameState();
    }

    private void Update()
    {
        if (_playerLifeController.PlayerAllLifeIsLost())
        {
            
        }
    }

    private void InitGameState()
    {
        // init
        CoroutineUtility.UStartCoroutine(_initTime, () => _gameStart = true);
        GameStateController.instance.GetState(GameStateType.Init).onUpdate += () =>
        {
            if (_gameStart)
                GameStateController.instance.ChangeState(GameStateType.Running);
        };

        // running
        GameStateController.instance.GetState(GameStateType.Running).onUpdate += () =>
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GameStateController.instance.ChangeState(GameStateType.Paused);
        };

        GameStateController.instance.GetState(GameStateType.Running).onUpdate += () =>
        {
            if (_door.playerEnterTheDoor && ObjectIsComplete())
            {
                GameStateController.instance.ChangeState(GameStateType.Succeed);
            }
        };

        GameStateController.instance.GetState(GameStateType.Running).onUpdate += () =>
        {
            if (_playerLifeController.PlayerAllLifeIsLost())
            {
                CoroutineUtility.UStartCoroutine(1f,
                    () => GameStateController.instance.ChangeState(GameStateType.Failure));
            }
        };

        // paused
        _pausedView.onExit +=
            () => GameStateController.instance.ChangeStateFromTo(GameStateType.Paused, GameStateType.Running);
    }

    public override bool ObjectIsComplete()
    {
        return _aliveEnemyCount == 0 && GameStateController.instance.currStateType == GameStateType.Running;
    }
}
