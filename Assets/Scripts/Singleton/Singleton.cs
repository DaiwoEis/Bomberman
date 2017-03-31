using System;

public class Singleton<T> where T : Singleton<T>
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
        _instance = (T)Activator.CreateInstance(typeof(T), true);

        _instance.Init();
    }

    public static void Destroy()
    {
        _instance.OnDestroy();

        _instance = null;
    }

    public virtual void Init()
    {
        
    }

    public virtual void OnDestroy()
    {
        
    }
}