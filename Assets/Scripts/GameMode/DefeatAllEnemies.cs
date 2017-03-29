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
    private BaseView _pausedView = null;

    [SerializeField]
    private PlayerLifeController _playerLifeController = null;

    private void Start()
    {
        _door = GameObject.FindWithTag(TagConfig.DOOR).GetComponent<Door>();

        InitGameState();
    }

    private void InitGameState()
    {
        // init
        CoroutineUtility.UStartCoroutine(1f, () => _gameStart = true);
        GameStateController.instance.GetState(GameStateType.Init).ChangeTo(() => _gameStart, GameStateType.Running);

        // running
        GameStateController.instance.GetState(GameStateType.Running)
            .ChangeTo(() => Input.GetKeyDown(KeyCode.Escape), GameStateType.Paused);

        GameStateController.instance.GetState(GameStateType.Running)
            .ChangeTo(() => _playerLifeController.PlayerAllLifeIsLost(), GameStateType.Failure);

        GameStateController.instance.GetState(GameStateType.Running)
            .ChangeTo(() => _door.playerEnterTheDoor && ObjectIsComplete(), GameStateType.Succeed);

        // paused
        _pausedView.onExit +=
            () => GameStateController.instance.ChangeStateEvent(GameStateType.Paused, GameStateType.Running);
    }

    public override bool ObjectIsComplete()
    {
        return _aliveEnemyCount == 0 && GameStateController.instance.currStateType == GameStateType.Running;
    }
}
