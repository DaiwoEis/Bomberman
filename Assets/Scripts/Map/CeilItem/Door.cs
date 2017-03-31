using UnityEngine;

public class Door : Actor
{
    private GameMode _gameMode = null;

    public bool playerEnterTheDoor { get; private set; }

    [SerializeField]
    private GameObject _lightPillar = null;

    private void Awake()
    {
        _lightPillar.SetActive(false);
    }

    private void Start()
    {
        TriggerOnSpawnEvent();

        _gameMode = GameObject.FindWithTag(TagConfig.GAME_MODE).GetComponent<GameMode>();
    }

    private void Update()
    {
        if (_gameMode.ObjectIsComplete())
        {
            _lightPillar.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagConfig.PLAYER) && Utility.IsArrive(other.transform.position, transform.position))
        {
            playerEnterTheDoor = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagConfig.PLAYER))
        {
            playerEnterTheDoor = false;
        }
    }
}
