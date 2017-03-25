using UnityEngine;

public class TriggerHitter : Function
{
    protected override void Start()
    {
        base.Start();

        _enabled = true;
    }

    protected override void TriggerEnter(Collider other)
    {
        IHitable hitTarget = other.GetComponent<IHitable>();
        if (hitTarget != null && hitTarget.CanHit(gameObject))
        {
            hitTarget.Hit(gameObject);
        }
    }
}
