using CUI;
using UnityEngine;
using UnityEngine.Analytics;

public class DefeatAllEnemies : GameMode
{
    [SerializeField]
    private int _aliveEnemyCount = 0;

    public int aliveEnemyCount { get { return _aliveEnemyCount; } set { _aliveEnemyCount = value; } }

    private bool _playerIsDead = false;

    [SerializeField]
    private bool _gameStart = false;

    public bool playerEnterTheDoor { get; set; }

    private Door _door = null;

    [SerializeField]
    private BaseView _pausedView = null;

    private bool _resumed = false;

    private void Start()
    {
        Singleton<ViewController>.Create();

        InitGameState();
    }

    private void InitGameState()
    {
        CoroutineUtility.UStartCoroutine(2f, () => _gameStart = true);
        GameObject.FindWithTag(TagConfig.PLAYER).GetComponent<Actor>().onDeath += () => _playerIsDead = true;
        _door = GameObject.FindWithTag(TagConfig.DOOR).GetComponent<Door>();
        _pausedView.onExit += () => { _resumed = true; };

        GameStateController.instance.When(GameStateType.Init).ChangeTo(() => _gameStart, GameStateType.Running);

        GameStateController.instance.When(GameStateType.Running)
            .ChangeTo(() => Input.GetKeyDown(KeyCode.Escape), GameStateType.Paused);
        GameStateController.instance.When(GameStateType.Running).ChangeTo(() => _playerIsDead, GameStateType.Failure);
        GameStateController.instance.When(GameStateType.Running).ChangeTo(() => _door.playerEnterTheDoor, GameStateType.Succeed);

        GameStateController.instance.When(GameStateType.Paused)
            .ChangeTo(() =>
            {
                bool result = _resumed;
                _resumed = false;
                return result;
            }, GameStateType.Running);
    }

    public override bool ObjectIsComplete()
    {
        return _aliveEnemyCount == 0 && GameStateController.instance.currStateType == GameStateType.Running;
    }

    private void OnDestroy()
    {
        Singleton<ViewController>.Destroy();
    }
}
