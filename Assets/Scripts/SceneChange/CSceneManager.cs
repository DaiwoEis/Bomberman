using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager 
{
    public static readonly string IntermidiateScene = "Intermediate Scene";

    public static readonly string MainMenuScene = "Main Menu";

    public static string NextScene = "";

    public static void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneChangeEffect effect = Camera.main.GetComponent<SceneChangeEffect>();
        if (effect != null)
        {
            effect.Run(() => SceneManager.LoadScene(IntermidiateScene));
        }
        else
        {
            SceneManager.LoadScene(IntermidiateScene);
        }
    }
}
