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

    private void OnTriggerEnter(Collider other)
    {
        if (_locked) return;

        if (other.CompareTag(TagConfig.PLAYER))
        {
            Singleton<GameState>.instance.GameOver(GameState.GameOverType.Succeed);
            Debug.Log("Game succeed");
        }
    }
}
