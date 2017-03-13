using UnityEngine;

public class ExplosionFlame : MonoBehaviour
{

	private void Start () 
	{
	    
	}

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitTarget = other.GetComponent<IHitable>();
        if (hitTarget != null && hitTarget.CanHit(gameObject))
        {
            hitTarget.Hit();
        }
    }
}
