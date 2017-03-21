using UnityEngine;

public class Door : Actor
{
    private bool _locked = true;

    private GameMode _gameMode = null;

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

        if (other.CompareTag(TagConfig.PLAYER) &&
            Utility.SqrtDistance(other.transform.position, transform.position) < Utility.ArriveSqrtMagnitude)
        {
            Singleton<GameState>.instance.GameOver(GameState.GameOverType.Succeed);
        }
    }
}
