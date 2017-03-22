using CUI;

public class GameOperationView : AnimateView 
{
    public void BackCallBack()
    {
        Singleton<ViewManager>.instance.AddCommond(new CloseCommond());
    }
}
