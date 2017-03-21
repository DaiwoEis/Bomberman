using UnityEngine;

public class AliveEnemyCounting : MonoBehaviour
{
    private static DefeatAllEnemies _gameMode = null;

    private void Awake()
    {
        if (_gameMode == null)
        {
            _gameMode = GameObject.FindWithTag(TagConfig.GAME_MODE).GetComponent<DefeatAllEnemies>();
        }

        Actor actor = GetComponent<Actor>();
        actor.onSpawn += () => _gameMode.aliveEnemyCount += 1;
        actor.onDeath += () => _gameMode.aliveEnemyCount -= 1;
    }  

    private void OnDestroy()
    {
        _gameMode = null;
    }
}
