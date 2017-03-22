using System;
using System.Collections;
using UnityEngine;

public class CoroutineUtility : MonoBehaviour 
{
    public static CoroutineUtility instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CoroutineUtility>();
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public static Coroutine UStartCoroutine(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }

    public static Coroutine UStartCoroutine(float time, Action action)
    {
        if (time.Equals(0f))
        {
            action();
            return null;
        }
        return instance.StartCoroutine(_TimeAction(time, action));
    }

    public static Coroutine UStartCoroutineReal(float time, Action action)
    {
        if (time.Equals(0f))
        {
            action();
            return null;
        }
        return instance.StartCoroutine(_TimeActionReal(time, action));
    }

    public static void UStopAllCoroutines()
    {
        instance.StopAllCoroutines();
    }

    private static IEnumerator _TimeAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        if (action != null)
        {
            action();
        }
    }

    private static IEnumerator _TimeActionReal(float time, Action action)
    {
        yield return new WaitForSecondsRealtime(time);
        if (action != null)
        {
            action();
        }
    }

    public static void UStopCoroutine(Coroutine routine)
    {
        instance.StopCoroutine(routine);
    }

    private void OnDestroy()
    {
        if (instance != null)
        {
            instance.StopAllCoroutines();
        }
    }
}
