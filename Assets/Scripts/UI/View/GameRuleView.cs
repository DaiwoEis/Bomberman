using CUI;
using UnityEngine;

public class GameRuleView : AnimateView
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
