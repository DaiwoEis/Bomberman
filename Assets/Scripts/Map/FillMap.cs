using UnityEngine;

[RequireComponent(typeof(CeilItem))]
[DefaultExecutionOrder(-1)]
public class FillMap : MonoBehaviour
{
    private CeilItem _ceilItem = null;

    private Map _map = null;

    private void Awake()
    {
        _ceilItem = GetComponent<CeilItem>();

        _map = GameObject.FindWithTag("Map").GetComponent<Map>();

        Actor actor = GetComponent<Actor>();
        actor.OnShow += () => _map.GetTile(transform.position).tileItems.Add(_ceilItem);
        actor.OnHide += () => _map.GetTile(transform.position).tileItems.Remove(_ceilItem);
    }
}
