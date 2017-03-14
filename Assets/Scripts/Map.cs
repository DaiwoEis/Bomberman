using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private TextAsset _mapAsset = null;

    [SerializeField]
    private string _emptyTileName = "n";

    [SerializeField]
    private string[] _mapDataName = null;

    [SerializeField]
    private GameObject[] _mapDataPrefab = null;

    [SerializeField]
    private GameObject[] _floorTilePrefabs = null;

    private Dictionary<string, GameObject> _mapDataDic = null;

    private int _width = 0;

    private int _height = 0;

    private string[,] _mapDatas = null;

    public bool[,] floor { get { return _floor; } }

    private bool[,] _floor = null;

    private void Start()
    {
        LoadMap();
    }

    private void LoadMap()
    {
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
        string[] lineDatas = _mapAsset.text.Split(new[] { "\r\n" }, options);
        string[] firstLineData = lineDatas[0].Split(new[] { "," }, options);
        _height = int.Parse(firstLineData[0]);
        _width = int.Parse(firstLineData[1]);
        _mapDatas = new string[_height, _width];
        _floor = new bool[_height, _width];
        for (int row = 0; row < _height; ++row)
        {
            string[] rowDatas = lineDatas[lineDatas.Length - 1 - row].Split(new[] { "," }, options);
            for (int col = 0; col < _width; ++col)
            {
                _mapDatas[row, col] = rowDatas[col];
                _floor[row, col] = rowDatas[col] != _emptyTileName;
            }
        }
    }

    private void SetUpMapDataDic()
    {
        _mapDataDic = new Dictionary<string, GameObject>();
        for (int i = 0; i < _mapDataName.Length; ++i)
        {
            _mapDataDic[_mapDataName[i]] = _mapDataPrefab[i];
        }
    }

    public void GenerateMap()
    {
        LoadMap();
        SetUpMapDataDic();
        GenerateMapData();
        GenerateFloor();
    }

    public void GenerateMapData()
    {
        ClearMapDatas();

        GameObject mapDatas = new GameObject("MapDatas");
        mapDatas.transform.parent = transform;
        for (int row = 0; row < _mapDatas.GetLength(0); ++row)
        {
            for (int col = 0; col < _mapDatas.GetLength(1); ++col)
            {
                if (_mapDatas[row, col] != _emptyTileName)
                {
                    GameObject go = Instantiate(_mapDataDic[_mapDatas[row, col]]);
                    go.transform.parent = mapDatas.transform;
                    go.transform.localPosition = new Vector3(col, 0f, row);
                }
            }
        }
    }

    public void GenerateFloor()
    {
        ClearFloor();

        Queue<GameObject> floorTileQueue = new Queue<GameObject>();

        foreach (GameObject tile in _floorTilePrefabs)
        {
            floorTileQueue.Enqueue(tile);
        }

        GameObject floor = new GameObject("Floor");
        floor.transform.parent = transform;
        for (int z = 0; z < _floor.GetLength(0); ++z)
        {
            if (z%2 == 0)
            {
                for (int x = 0; x < _floor.GetLength(1); ++x)
                {
                    GameObject tilePrefab = floorTileQueue.Dequeue();
                    GameObject newTile = Instantiate(tilePrefab);
                    newTile.transform.parent = floor.transform;
                    newTile.transform.localPosition = new Vector3(x, 0f, z);
                    floorTileQueue.Enqueue(tilePrefab);
                }
            }
            else
            {
                for (int x = _floor.GetLength(1) - 1; x >= 0; --x)
                {
                    GameObject tilePrefab = floorTileQueue.Dequeue();
                    GameObject newTile = Instantiate(tilePrefab);
                    newTile.transform.parent = floor.transform;
                    newTile.transform.localPosition = new Vector3(x, 0f, z);
                    floorTileQueue.Enqueue(tilePrefab);
                }
            }            
        }
    }

    public void ClearMapDatas()
    {
        Transform floor = transform.Find("MapDatas");
        if (floor != null)
        {
            DestroyImmediate(floor.gameObject);
        }
    }

    public void ClearFloor()
    {
        Transform floor = transform.Find("Floor");
        if (floor != null)
        {
            DestroyImmediate(floor.gameObject);
        }
    }

    public bool CanPlace(Vector3 pos)
    {
        int row = (int)pos.z;
        int col = (int)pos.x;
        return _floor[row, col] == false;
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

    private void OnDrawGizmos()
    {
        if (_floor != null)
        {
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            for (int z = 0; z < _height; ++z)
            {
                for (int x = 0; x < _width; ++x)
                {
                    if (_floor[z, x])
                    {
                        Gizmos.DrawCube(new Vector3(x, 3f, z), Vector3.one * 0.7f);
                    }
                }
            }
        }
    }
}
