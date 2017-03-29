using CUI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private BaseView _mainMenuView = null;

    private void Start()
    {
        ViewController.Create();

        Singleton<ViewController>.instance.AddCommond(new OpenCommond(_mainMenuView));
    }

    private void OnDestroy()
    {
        ViewController.Destroy();
    }

}