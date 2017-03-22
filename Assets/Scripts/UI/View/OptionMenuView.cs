using CUI;
using UnityEngine;

public class OptionMenuView : AnimateView 
{

    //public void SoundCallBack()
    //{
    //    Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
    //    Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.SoundSetting));
    //}

    public void BackCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
    }

    public override void OnUpdate(UIType uiType)
    {
        base.OnUpdate(uiType);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
        }
    }
}
