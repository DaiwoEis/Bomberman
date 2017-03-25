using UnityEngine;

public class Door : Actor
{
    private bool _locked = true;

    private GameMode _gameMode = null;

    public bool playerEnterTheDoor { get; private set; }

    private void Start()
    {
        TriggerOnSpawnEvent();

        _gameMode = GameObject.FindWithTag(TagConfig.GAME_MODE).GetComponent<GameMode>();
    }

    private void Update()
    {
        if (_gameMode.ObjectIsComplete())
        {
            _locked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_locked) return;

        if (other.CompareTag(TagConfig.PLAYER) && Utility.IsArrive(other.transform.position, transform.position))
        {
            playerEnterTheDoor = true;
            _locked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_locked) return;

        if (other.CompareTag(TagConfig.PLAYER))
        {
            playerEnterTheDoor = false;
        }
    }
}
