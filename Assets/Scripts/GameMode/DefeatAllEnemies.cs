using UnityEngine;

public class DefeatAllEnemies : GameMode
{
    [SerializeField]
    private int _aliveEnemyCount = 0;

    public int aliveEnemyCount { get { return _aliveEnemyCount; } set { _aliveEnemyCount = value; } }

    private void Start()
    {
        GameObject.FindWithTag(TagConfig.PLAYER).GetComponent<Actor>().onDeath += () =>
        {
            Singleton<GameState>.instance.GameOver(GameState.GameOverType.Failure);
        };
    }

    public override bool ObjectIsComplete()
    {
        return _aliveEnemyCount == 0 && Singleton<GameState>.instance.state == GameState.GameStage.Running;
    }
}
