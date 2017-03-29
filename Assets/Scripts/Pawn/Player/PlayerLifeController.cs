using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField]
    private int _playerDefaultLife = 3;

    [SerializeField]
    private Transform _playerRespawnPoint = null;

    [SerializeField]
    private int _playerLife = -1;

    public int playerLife { get { return _playerLife; } }

    private GameObject _player = null;

    private static readonly string PLAYER_LIFE = "PlayerLife";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PLAYER_LIFE))
        {
            _playerLife = _playerDefaultLife;
            PlayerPrefs.SetInt(PLAYER_LIFE, _playerLife);
        }
        else
        {
            _playerLife = PlayerPrefs.GetInt(PLAYER_LIFE);
        }

        _player = GameObject.FindWithTag(TagConfig.PLAYER);
        _player.GetComponent<Actor>().onDeath += () =>
        {
            _playerLife -= 1;
            if (!PlayerAllLifeIsLost())
                Invoke("RespawnPlayer", 0.1f);
        };
    }

    private void Start()
    {
        GameStateController.instance.GetState(GameStateType.Succeed).onEnter += () =>
        {
            PlayerPrefs.SetInt(PLAYER_LIFE, _playerLife);
        };
    }

    public bool PlayerAllLifeIsLost()
    {
        return _playerLife == 0;
    }

    private void RespawnPlayer()
    {
        _player.transform.position = _playerRespawnPoint.position;
        _player.transform.rotation = _playerRespawnPoint.rotation;
        _player.GetComponent<Actor>().TriggerOnSpawnEvent();
    }
}
