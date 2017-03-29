using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                Create();
            }
            return _instance;
        }
    }

    public static void Create()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                Debug.LogError(string.Format("Please make at least exit a {0} GameObject in the scene", typeof(T)));
                return;
            }

            _instance.Init();
        }
    }

    public static void Destroy()
    {
        _instance = null;
    }

    protected virtual void Init()
    {
        
    }
}
