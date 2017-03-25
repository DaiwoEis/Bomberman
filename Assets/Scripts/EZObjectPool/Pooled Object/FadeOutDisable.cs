using System.Collections.Generic;
using EZObjectPools;
using UnityEngine;

public class FadeOutDisable : PooledObject 
{
    [SerializeField]
    private MeshRenderer[] _renderers = null;

    [SerializeField]
    private float _fadeOutTime = 1f;

    private float _timer;

    private Dictionary<Material, float> _materials = new Dictionary<Material, float>();

    protected override void Awake ()
    {
        base.Awake();

        foreach (MeshRenderer meshRenderer in _renderers)
        {
            foreach (Material material in meshRenderer.materials)
            {
                _materials.Add(meshRenderer.material, meshRenderer.material.color.a);
            }
        }
    }

    public override void OnRetrieveFromPool()
    {
        base.OnRetrieveFromPool();

        foreach (Material material in _materials.Keys)
        {
            Color originColor = material.color;
            originColor.a = _materials[material];
            material.color = originColor;
        }
        _timer = 0f;
    }

    protected override void Update ()
    {
        base.Update();

        if (_timer > _fadeOutTime)
        {
            return;
        }

        float t = _timer/_fadeOutTime;
        t = Mathf.Clamp01(t);
        foreach (Material material in _materials.Keys)
        {
            Color newColor = material.color;
            newColor.a = Mathf.Lerp(_materials[material], 0f, t);
            material.color = newColor;
        }

        _timer += Time.deltaTime;
        if (_timer > _fadeOutTime)
        {
            ReturnToPool();
        }
    }
}
