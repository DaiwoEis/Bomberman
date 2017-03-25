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
        GameStateController.instance.OnGameSucceed += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_succeedView));
        };

        GameStateController.instance.OnGameFailure += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_failuredView));
        };

        GameStateController.instance.OnGamePaused += () =>
        {
            Singleton<ViewController>.instance.AddCommond(new OpenCommond(_pausedView));
        };
    }
}
