using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField]
    private int _playerDefaultLife = 3;

    [SerializeField]
    private Transform _playerRespawnPoint = null;

    private GameObject _player = null;

    private static int playerLifeCount = -1;

    public int playerLife { get { return playerLifeCount; } }

    private int _currLevelPlayerLifeCount = 0;

    private void Awake()
    {       
        if (playerLifeCount == -1)
        {
            playerLifeCount = _playerDefaultLife;
        }

        _currLevelPlayerLifeCount = playerLifeCount;

        _player = GameObject.FindWithTag(TagConfig.PLAYER);
        _player.GetComponent<Actor>().onDeath += () =>
        {
            playerLifeCount -= 1;
            if (!PlayerAllLifeIsLost())
                Invoke("RespawnPlayer", 0.1f);
        };
    }

    private void Start()
    {
        GameStateController.instance.GetState(GameStateType.Failure).onEnter += () =>
        {
            playerLifeCount = _currLevelPlayerLifeCount;
        };
    }

    public bool PlayerAllLifeIsLost()
    {
        return playerLifeCount == 0;
    }

    private void RespawnPlayer()
    {
        _player.transform.position = _playerRespawnPoint.position;
        _player.transform.rotation = _playerRespawnPoint.rotation;
        _player.GetComponent<Actor>().TriggerOnSpawnEvent();
    }
}
