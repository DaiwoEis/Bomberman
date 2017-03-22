using System;
using System.Collections;
using UnityEngine;

public class SceneChangeEffect : MonoBehaviour
{
    [SerializeField]
    public Material _effectMaterial;

    private bool _start = false;

    [SerializeField]
    private float _effecTime = 2f;

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (_start)
        {
            Graphics.Blit(src, dst, _effectMaterial);
        }
        else
        {
            Graphics.Blit(src, dst);
        }          
    }

    public void Run(Action onComplete)
    {
        StartCoroutine(_Run(onComplete));
    }

    public void RunReverse()
    {
        StartCoroutine(_RunReverse());
    }

    private IEnumerator _Run(Action onComplete)
    {
        _start = true;
        float timer = 0f;
        while (timer < _effecTime)
        {
            _effectMaterial.SetFloat("_Magnitude", timer/_effecTime*0.5f);
            yield return null;
            timer += Time.unscaledDeltaTime;
        }
        onComplete();
    }

    private IEnumerator _RunReverse()
    {
        _start = true;
        float timer = 0f;
        while (timer < _effecTime)
        {
            _effectMaterial.SetFloat("_Magnitude", 1f - timer/_effecTime);
            yield return null;
            timer += Time.unscaledDeltaTime;
        }
        _start = false;
    }
}