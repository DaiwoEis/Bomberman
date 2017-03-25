using UnityEngine;

public class Function : MonoBehaviour
{
    [SerializeField]
    protected bool _enabled = false;        

	protected virtual void Awake() { }

    protected virtual void Start() { }

    protected void Update()
    {
        if (_enabled == false) return;

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (_enabled == false) return;

        TriggerEnter(other);
    }

    protected virtual void TriggerEnter(Collider other)
    {
        
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (_enabled == false) return;

        CollisionEnter(other);
    }

    protected virtual void CollisionEnter(Collision other)
    {
        
    }

    public virtual void On()
    {
        _enabled = true;
    }

    public virtual void Off()
    {
        _enabled = false;
    }
}
