using CUI;
using UnityEngine;

public class AboutView : AnimateView 
{
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
