using UnityEngine;

public class AliveEnemyCounting : MonoBehaviour
{
    private DefeatAllEnemies _gameMode = null;

    private void Awake()
    {
        _gameMode = GameObject.FindWithTag(TagConfig.GAME_MODE).GetComponent<DefeatAllEnemies>();

        Actor actor = GetComponent<Actor>();
        actor.onSpawn += () => _gameMode.aliveEnemyCount += 1;
        actor.onDeath += () => _gameMode.aliveEnemyCount -= 1;
    }  

    private void OnDestroy()
    {
        _gameMode = null;
    }
}
