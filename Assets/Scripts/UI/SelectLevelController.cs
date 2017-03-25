using UnityEngine;

public class SelectLevelController : MonoBehaviour
{
    public void SelectLevelCallBack(string levelName)
    {
        CoroutineUtility.UStartCoroutineReal(0.5f, () => CSceneManager.LoadScene(levelName));
    }
}
