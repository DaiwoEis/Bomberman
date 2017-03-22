
namespace CUI
{
    public class UIType
    {
        public string Path { get; private set; }

        public string Name { get; private set; }

        public UIType(string path)
        {
            Path = path;
            Name = path.Substring(path.LastIndexOf('/') + 1);
        }

        public override string ToString()
        {
            return string.Format("path : {0} name : {1}", Path, Name);
        }

        public static readonly UIType MainMenu = new UIType("View/MainMenuView");
        public static readonly UIType OptionMenu = new UIType("View/OptionMenuView");
        public static readonly UIType SoundSetting = new UIType("View/SoundSettingView");
        public static readonly UIType About = new UIType("View/AboutView");
        public static readonly UIType GamePaused = new UIType("View/GamePausedView");
        public static readonly UIType GameRun = new UIType("View/GameRunView");
        public static readonly UIType GameOver = new UIType("View/GameOverView");
        public static readonly UIType GameSucceed = new UIType("View/GameSucceedView");
        public static readonly UIType SlectLevel = new UIType("View/SelectLevelView");
        public static readonly UIType GameOperation = new UIType("View/GameOperationView");
        public static readonly UIType GameRule = new UIType("View/GameRuleView");
        public static readonly UIType GameHelp = new UIType("View/GameHelpView");
    }
}
