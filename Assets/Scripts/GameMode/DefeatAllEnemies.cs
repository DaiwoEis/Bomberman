using CUI;
using UnityEngine;

public class DefeatAllEnemies : GameMode
{
    [SerializeField]
    private int _aliveEnemyCount = 0;

    public int aliveEnemyCount { get { return _aliveEnemyCount; } set { _aliveEnemyCount = value; } }

    private void Start()
    {
        GameState.instance.GameInit();

        Singleton<ViewManager>.Create();

        GameObject.FindWithTag(TagConfig.PLAYER).GetComponent<Actor>().onDeath += () =>
        {
            GameState.instance.GameOver(GameState.GameOverType.Failure);
        };

        CoroutineUtility.UStartCoroutine(2f, () =>
        {
            GameState.instance.GameStart();
            Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameRun));
        });
    }

    public override bool ObjectIsComplete()
    {
        return _aliveEnemyCount == 0 && GameState.instance.state == GameState.GameStage.Running;
    }

    private void OnDestroy()
    {
        Singleton<ViewManager>.Destroy();
    }
}
