using System;
using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private TextAsset _mapAsset = null;

    private bool[,] _floor = null;

	private void Awake() { }

    private void Start()
    {
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;        
        string[] lineDatas = _mapAsset.text.Split(new[] {"\r\n"}, options);
        string[] firstLine = lineDatas[0].Split(new[] {" "}, options);   
        _floor = new bool[int.Parse(firstLine[0]), int.Parse(firstLine[1])];
        for (int row = 0; row < _floor.GetLength(0); ++row)
        {
            string[] rowDatas = lineDatas[lineDatas.Length - 1 - row].Split(new[] { "," }, options);
            for (int col = 0; col < _floor.GetLength(1); ++col)
            {                
                switch (char.Parse(rowDatas[col]))
                {
                    case 'n':
                        _floor[row, col] = false;
                        break;
                    default:
                        _floor[row, col] = true;
                        break;
                }
            }
        }
    }

    private void Update() { }

    public bool CanPlace(Vector3 pos)
    {
        int row = (int) pos.z;
        int col = (int) pos.x;
        return !_floor[row, col];
    }

    public void FillMap(Vector3 pos)
    {
        int row = (int)pos.z;
        int col = (int)pos.x;
        _floor[row, col] = true;
    }

    public void UnFillMap(Vector3 pos)
    {
        int row = (int)pos.z;
        int col = (int)pos.x;
        _floor[row, col] = false;
    }

    public void OnDrawGizmos()
    {
        if (_floor != null)
        {
            Gizmos.color = Color.blue;
            for (int row = 0; row < _floor.GetLength(0); ++row)
            {
                for (int col = 0; col < _floor.GetLength(1); ++col)
                {
                    if (_floor[row, col])
                    {
                        Gizmos.DrawCube(new Vector3(col, 1f, row), Vector3.one * 0.8f);
                    }
                }
            }
        }
    }
}
