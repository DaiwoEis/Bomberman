using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private static string FLOOR_NAME = "Floor";

    private static string TILES_NAME = "Tiles";

    private static string TILE_ITEMS_FILE_NAME = "TileItems";

    [SerializeField]
    private TextAsset _mapAsset = null;

    [SerializeField]
    private GameObject[] _floorTilePrefabs = null;

    [SerializeField]
    private int _width = 0;

    [SerializeField]
    private int _height = 0;

    private Tile[,] _tiles = null;

    public Tile[,] tiles { get { return _tiles; } }

    private Dictionary<string, GameObject> _tileItemPrefabDic = null;

    private void Start()
    {
        Transform _tilesTrans = transform.Find(TILES_NAME);
        _tiles = new Tile[_height, _width];
        for (int z = 0; z < _height; ++z)
        {
            for (int x = 0; x < _width; ++x)
            {
                int index = z*_width + x;
                _tiles[z, x] = _tilesTrans.GetChild(index).GetComponent<Tile>();                
            }
        }
    }

    public Vector3 GetCenterPosition(Vector3 pos)
    {
        pos.x = Mathf.Round(pos.x);
        pos.y = 0f;
        pos.z = Mathf.Round(pos.z);
        return pos;
    }

    public Tile GetTile(Vector3 pos)
    {
        Vector3 centerPos = GetCenterPosition(pos);
        return _tiles[(int) centerPos.z, (int) centerPos.x];
    }

    public bool IsEmptyTile(Vector3 pos)
    {
        return GetTile(pos).tileItems.Count == 0;
    }

    public void GenerateMap()
    {
        GetAllMapItemPrefab();

        GenrateTiles();
        GenerateFloor();
    }

    private void GetAllMapItemPrefab()
    {
        _tileItemPrefabDic = new Dictionary<string, GameObject>();
        foreach (TileItem tileItem in Resources.LoadAll<TileItem>(TILE_ITEMS_FILE_NAME))
        {
            _tileItemPrefabDic[tileItem.name] = tileItem.gameObject;
        }
    }

    private void GenrateTiles()
    {
        ClearTiles();

        GameObject tilesGO = new GameObject(TILES_NAME);
        tilesGO.transform.parent = transform;

        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
        string[] lineDatas = _mapAsset.text.Split(new[] { "\r\n" }, options);
        string[] firstLineData = lineDatas[0].Split(new[] { "," }, options);
        _height = int.Parse(firstLineData[0]);
        _width = int.Parse(firstLineData[1]);
        for (int z = 0; z < _height; ++z)
        {
            string[] rowDatas = lineDatas[lineDatas.Length - 1 - z].Split(new[] { "," }, options);
            for (int x = 0; x < _width; ++x)
            {
                GameObject newTileGO = new GameObject("Tile");
                newTileGO.transform.parent = tilesGO.transform;
                newTileGO.transform.SetAsLastSibling();
                Vector3 centerPos = new Vector3(x, 0f, z);
                newTileGO.transform.position = centerPos;
                Tile newTile = newTileGO.AddComponent<Tile>();
                newTile.centerPosition = centerPos;

                if (_tileItemPrefabDic.ContainsKey(rowDatas[x]))
                {
                    TileItem tileItem =
                        Instantiate(_tileItemPrefabDic[rowDatas[x]], centerPos, Quaternion.identity)
                            .GetComponent<TileItem>();
                    tileItem.transform.parent = newTileGO.transform;
                    newTile.tileItems.Add(tileItem);                             
                }
            }        
        }
    }

    private void GenerateFloor()
    {
        ClearFloor();

        Queue<GameObject> floorTileQueue = new Queue<GameObject>();

        foreach (GameObject tile in _floorTilePrefabs)
        {
            floorTileQueue.Enqueue(tile);
        }

        GameObject floorGO = new GameObject("Floor");
        floorGO.transform.parent = transform;

        for (int z = 0; z < _height; ++z)
        {
            if (z%2 == 0)
            {
                for (int x = 0; x < _width; ++x)
                {
                    GameObject tilePrefab = floorTileQueue.Dequeue();
                    GameObject newTile = Instantiate(tilePrefab);
                    newTile.transform.parent = floorGO.transform;
                    newTile.transform.localPosition = new Vector3(x, 0f, z);
                    floorTileQueue.Enqueue(tilePrefab);
                }
            }
            else
            {
                for (int x = _width - 1; x >= 0; --x)
                {
                    GameObject tilePrefab = floorTileQueue.Dequeue();
                    GameObject newTile = Instantiate(tilePrefab);
                    newTile.transform.parent = floorGO.transform;
                    newTile.transform.localPosition = new Vector3(x, 0f, z);
                    floorTileQueue.Enqueue(tilePrefab);
                }
            }            
        }
    }

    private void ClearTiles()
    {
        Transform floor = transform.Find(TILES_NAME);
        if (floor != null)
        {
            DestroyImmediate(floor.gameObject);
        }
    }

    private void ClearFloor()
    {
        Transform floor = transform.Find(FLOOR_NAME);
        if (floor != null)
        {
            DestroyImmediate(floor.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (_tiles != null)
        {
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            for (int z = 0; z < _height; ++z)
            {
                for (int x = 0; x < _width; ++x)
                {
                    if (!IsEmptyTile(new Vector3(x, 0f, z)))
                    {
                        Gizmos.DrawCube(new Vector3(x, 3f, z), Vector3.one*0.7f);
                    }
                }
            }
        }
    }
}
