using System.Collections;
using UnityEngine;

namespace CUI
{
    public class GamePausedView : AnimateView 
    {

        public override IEnumerator _OnEnter(UIType uiType)
        {
            GameState.instance.GamePaused();
            yield return base._OnEnter(uiType);
        }

        public override void OnUpdate(UIType uiType)
        {
            base.OnUpdate(uiType);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Singleton<ViewManager>.instance.AddCommond(new CloseCommond(() =>
                {
                    GameState.instance.GameResumed();
                }));
            }
        }

        public void OptionsCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
            Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.OptionMenu));
        }

        public void HelpCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
            Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameHelp));
        }

        public void BackCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(
                new CloseAllCommond(() => CSceneManager.LoadScene(CSceneManager.MainMenuScene)));
        }
    }
}