using System.Linq;
using UnityEngine;

public class PawnHitableBody : Function, IHitable
{
    [SerializeField]
    private string[] _canHittedTags = null;

    public bool CanHit(GameObject hitter)
    {
        return _canHittedTags.Any(hitter.CompareTag) && _enabled;
    }

    public void Hit(GameObject hitter)
    {
        GetComponent<Pawn>().Death();
        Debug.Log(hitter.name + " hit " + gameObject.name);
    }
}
