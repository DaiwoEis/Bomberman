using UnityEngine;
using EZObjectPools;

[AddComponentMenu("EZ Object Pools/Pooled Objects/Timed Disable")]
public class TimedDisable : PooledObject
{
    [SerializeField]
    private float _disableTime = 3f;

    public override void OnRetrieveFromPool()
    {
        base.OnRetrieveFromPool();

        CoroutineUtility.UStartCoroutine(_disableTime, ReturnToPool);
    }
}
