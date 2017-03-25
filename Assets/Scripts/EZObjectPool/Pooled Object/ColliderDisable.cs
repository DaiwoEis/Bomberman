using System.Collections;
using EZObjectPools;
using UnityEngine;

public class ColliderDisable : PooledObject
{
    [SerializeField]
    private float _disableTime = 3f;

    [SerializeField]
    private string _collideTag = "";

    [SerializeField]
    private float _maxLiveTime = 10f;

    private Rigidbody _rigidbody = null;

    private bool _isCollide = false;

    private Coroutine _aliveCoroutine = null;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_collideTag))
        {
            _isCollide = true;
            CoroutineUtility.UStartCoroutine(_disableTime, ReturnToPool);
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(_collideTag))
        {
            _isCollide = true;
            CoroutineUtility.UStartCoroutine(_disableTime, ReturnToPool);
        }
    }

    public override void OnRetrieveFromPool()
    {
        base.OnRetrieveFromPool();

        if (_aliveCoroutine != null)
            StopCoroutine(_aliveCoroutine);
        _aliveCoroutine = StartCoroutine(_ReturlToPool());
        _isCollide = false;
    }

    public override void OnReturnToPool()
    {
        base.OnReturnToPool();

        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private IEnumerator _ReturlToPool()
    {
        yield return new WaitForSeconds(_maxLiveTime);
        if(!_isCollide) ReturnToPool();
        _aliveCoroutine = null;
    }
}
