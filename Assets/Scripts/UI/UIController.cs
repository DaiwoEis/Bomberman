using CUI;
using UnityEngine;

public class UIController : MonoBehaviour 
{
    public void AddOpenCommend(BaseView nextView)
    {
        Singleton<ViewController>.instance.AddCommond(new OpenCommond(nextView));
    }

    public void AddCloseCommend()
    {
        Singleton<ViewController>.instance.AddCommond(new CloseCommond());
    }

    public void AddCloseAllCommend()
    {
        Singleton<ViewController>.instance.AddCommond(new CloseAllCommond());
    }

    public void AddPausedCommend()
    {
        Singleton<ViewController>.instance.AddCommond(new PauseCommond());
    }
}
