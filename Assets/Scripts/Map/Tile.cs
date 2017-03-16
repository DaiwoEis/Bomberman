using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private List<TileItem> _tileItems = new List<TileItem>();

    public List<TileItem> tileItems { get { return _tileItems; } }

    public Vector3 centerPosition { get; set; }
}
