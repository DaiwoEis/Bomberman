using CUI;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    [SerializeField]
    private BaseView _pausedView = null;

    [SerializeField]
    private BaseView _succeedView = null;

    [SerializeField]
    private BaseView _failuredView = null;

    private void Awake()
    {
        GameStateController.instance.GetState(GameStateType.Succeed).onEnter += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_succeedView));
        };

        GameStateController.instance.GetState(GameStateType.Failure).onEnter += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_failuredView));
        };

        GameStateController.instance.GetState(GameStateType.Paused).onEnter += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_pausedView));
        };
    }
}
