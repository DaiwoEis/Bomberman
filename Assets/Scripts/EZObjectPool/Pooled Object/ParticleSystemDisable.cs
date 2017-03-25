using UnityEngine;
using EZObjectPools;

[AddComponentMenu("EZ Object Pool/Pooled Objects/Particle System Disable")]
public class ParticleSystemDisable : PooledObject 
{
    private ParticleSystem[] _particles;

    private float _maxDuration;

    protected override void Awake()
    {
        base.Awake();

        if(_particles == null)
            _particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem system in _particles)
        {
            ParticleSystem.MainModule main = system.main;
            main.playOnAwake = false;
            if (main.duration > _maxDuration)
                _maxDuration = main.duration;
        }
    }

    public override void OnRetrieveFromPool()
    {
        base.OnRetrieveFromPool();

        foreach (ParticleSystem system in _particles)
        {
            system.Play();
        }
        CoroutineUtility.UStartCoroutine(_maxDuration, ReturnToPool);
    }
}
