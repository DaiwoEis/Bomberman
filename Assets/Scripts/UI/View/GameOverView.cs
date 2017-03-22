using UnityEngine;

namespace CUI
{
    public class GameOverView : AnimateView
    {
        [SerializeField]
        private string _levelName = "";

        public void RestartCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseAllCommond(() => CSceneManager.LoadScene(_levelName)));
        }

        public void BackCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseAllCommond(() => CSceneManager.LoadScene(CSceneManager.MainMenuScene)));
        }
    }
}