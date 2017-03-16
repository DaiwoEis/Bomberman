using UnityEngine;

[RequireComponent(typeof(TileItem))]
public class FillMapWithInTime : MonoBehaviour
{
    private TileItem _tileItem = null;

    private void Awake()
    {
        _tileItem = GetComponent<TileItem>();
    }

    private void OnEnable()
    {
        GameObject.Find("Map").GetComponent<Map>().GetTile(transform.position).tileItems.Add(_tileItem);
    }

    private void Update() { }

    private void OnDisable()
    {
        GameObject.Find("Map").GetComponent<Map>().GetTile(transform.position).tileItems.Remove(_tileItem);
    }
}
