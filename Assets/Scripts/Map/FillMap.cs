using UnityEngine;

[RequireComponent(typeof(CeilItem))]
public class FillMap : MonoBehaviour
{
    private CeilItem _ceilItem = null;

    private void Awake()
    {
        _ceilItem = GetComponent<CeilItem>();

        Map map = Singleton<Map>.instance;

        Actor actor = GetComponent<Actor>();
        actor.onSpawn += () => map.GetCeil(transform.position).items.Add(_ceilItem);
        actor.onDeath += () => map.GetCeil(transform.position).items.Remove(_ceilItem);
    }
}
