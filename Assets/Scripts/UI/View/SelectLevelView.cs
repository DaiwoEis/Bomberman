using CUI;
using UnityEngine;

public class SelectLevelView : AnimateView
{
    public override void OnUpdate(UIType uiType)
    {
        base.OnUpdate(uiType);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
        }
    }

    public void BackCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
    }

    public void SelectLevelCallBack(string levelName)
    {
        Singleton<ViewManager>.instance.AddCommond(new CloseAllCommond(() => CSceneManager.LoadScene(levelName)));
    }

    public void LockedCallBack()
    {
        
    }
}
