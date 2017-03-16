using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private List<TileItem> _tileItems = new List<TileItem>();

    public List<TileItem> tileItems { get { return _tileItems; } }

    public Vector3 centerPosition { get; set; }

    public TileIndex tileIndex { get { return new TileIndex((int) centerPosition.z, (int) centerPosition.x); } }
}

public struct TileIndex
{
    public TileIndex(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public int row;
    public int col;
}