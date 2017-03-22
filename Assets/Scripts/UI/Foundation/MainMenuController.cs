using UnityEngine;

namespace CUI
{
	public class MainMenuController : MonoBehaviour {

        private void Start()
        {
            Singleton<ViewManager>.Create();

            Singleton<ViewManager>.instance.AddCommond(new OpenCommond(UIType.MainMenu));
        }

	    private void OnDestroy()
	    {
            Singleton<ViewManager>.Destroy();
        }

	}
}
