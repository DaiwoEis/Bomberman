using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CUI
{
    public class GameSucceedView : AnimateView 
    {

        [SerializeField]
        private string _nextLevelName = "";

        public void NextLevelCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseAllCommond(() => CSceneManager.LoadScene(_nextLevelName)));
        }

        public void BackCallBack()
        {
            Singleton<ViewManager>.instance.AddCommond(new CloseAllCommond(() => CSceneManager.LoadScene(CSceneManager.MainMenuScene)));
        }

    }
}