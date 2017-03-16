using UnityEngine;

[RequireComponent(typeof(TileItem))]
public class FillMapWithInTime : MonoBehaviour
{
    private TileItem _tileItem = null;

    private Map _map = null;

    private void Awake()
    {
        _tileItem = GetComponent<TileItem>();

        _map = GameObject.Find("Map").GetComponent<Map>();
    }

    private void OnEnable()
    {
        _map.GetTile(transform.position).tileItems.Add(_tileItem);
    }

    private void Update() { }

    private void OnDisable()
    {
        _map.GetTile(transform.position).tileItems.Remove(_tileItem);
    }
}
