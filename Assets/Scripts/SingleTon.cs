using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                Debug.LogError(string.Format("Please make at least exit a {0} GameObject in the scene", typeof(T)));
            }

            return _instance;

        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}
