using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntermediateSceneController : MonoBehaviour
{
    [SerializeField]
    private Slider _slider = null;

	private void Start ()
	{
	    StartCoroutine(LoadScene());
	}

    private IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(CSceneManager.NextScene, LoadSceneMode.Additive);
        while (!async.isDone)
        {          
            _slider.value = async.progress;         
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(CSceneManager.NextScene));
        SceneManager.UnloadSceneAsync(CSceneManager.IntermidiateScene);
        CSceneManager.NextScene = "";
        SceneChangeEffect effect = Camera.main.GetComponent<SceneChangeEffect>();
        if (effect != null)
            effect.RunReverse();
    }

}
