using System.Collections.Generic;
using UnityEngine;

public class Ceil : MonoBehaviour
{
    [SerializeField]
    private List<CeilItem> _items = new List<CeilItem>();

    public List<CeilItem> items { get { return _items; } }

    public CeilCoordinate coordinate { get; set; }

    public Vector3 centerPosition { get { return new Vector3(coordinate.col, 0f, coordinate.row); } }
}

public struct CeilCoordinate
{
    public CeilCoordinate(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public int row;
    public int col;
}