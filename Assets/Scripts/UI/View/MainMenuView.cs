using CUI;
using UnityEngine;

public class MainMenuView : AnimateView
{
    public void StartGameCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
        Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.SlectLevel));
    }

    public void OptionCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
        Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.OptionMenu));
    }

    public void AboutCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
        Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.About));
    }

    public void QuitCallBack()
    {
        Application.Quit();
    }
    
}
