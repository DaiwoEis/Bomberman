using CUI;

public class GameHelpView : AnimateView 
{
    public void OperationCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
        Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameOperation));
    }

    public void RuleCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
        Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameRule));
    }

    public void BackCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
    }
}
