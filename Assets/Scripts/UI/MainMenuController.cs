using CUI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private BaseView _mainMenuView = null;

    private void Start()
    {
        Singleton<ViewController>.Create();

        Singleton<ViewController>.instance.AddCommond(new OpenCommond(_mainMenuView));
    }

    private void OnDestroy()
    {
        Singleton<ViewController>.Destroy();
    }

}