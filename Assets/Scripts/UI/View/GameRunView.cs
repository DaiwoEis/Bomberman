using UnityEngine;

namespace CUI
{
    public class GameRunView : AnimateView
    {
        protected override void Awake()
        {
            base.Awake();

            GameState.instance.OnGameOver += OnGameOver;
        }

        public override void OnUpdate(UIType uiType)
        {
            base.OnUpdate(uiType);

            if (GameState.instance.state != GameState.GameStage.Running) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
                Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GamePaused));
            }
        }

        private void OnGameOver(GameState.GameOverType type)
        {
            if (type == GameState.GameOverType.Failure)
            {
                Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
                Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameOver));
            }
            else
            {
                Singleton<ViewManager>.instance.AddCommond(new PauseCommond());
                Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.GameSucceed));
            }
        }
    }
}