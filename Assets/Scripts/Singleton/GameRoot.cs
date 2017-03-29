using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField]
    private TextAsset _singletonConfig = null;

    private void Awake()
    {
        InvokeMethod("Create");
    }

    private void OnDestroy()
    {
        InvokeMethod("Destroy");
    }

    private void InvokeMethod(string methodName)
    {
        foreach (string singletonName in GetSingletonNames())
        {
            Type type = Type.GetType(singletonName);

            if (type == null)
            {
                Debug.LogError(string.Format("type name: {0} is not validate", singletonName));
                continue;
            }

            type.BaseType
                .GetMethod(methodName, BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, null);
        }
    }

    private IEnumerable<string> GetSingletonNames()
    {
        return _singletonConfig.text.Split(',');
    }
}
