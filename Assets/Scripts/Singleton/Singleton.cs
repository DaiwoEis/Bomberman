using System;

public static class Singleton<T> where T : class
{
    private static T _instance;

    public static void Create()
    {
        _instance = (T)Activator.CreateInstance(typeof(T), true);
    }

    public static T instance
    {
        get
        {
            return _instance;
        }
    }

    public static void Destroy()
    {
        _instance = null;
    }
}