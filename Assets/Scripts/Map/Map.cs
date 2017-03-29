using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoSingleton<Map>
{
    private static readonly string FLOOR_NAME = "Floor";

    private static readonly string CEILS_NAME = "Ceils";

    private static readonly string CEIL_ITEMS_FILE_NAME = "CeilItems";

    [SerializeField]
    private TextAsset _mapAsset = null;

    [SerializeField]
    private GameObject[] _floorTilePrefabs = null;

    [SerializeField]
    private int _width = 0;

    public int width { get { return _width; } }

    [SerializeField]
    private int _height = 0;

    public int height { get { return _height; } }

    private Ceil[,] _ceils = null;

    private Dictionary<string, GameObject> _tileItemPrefabDic = null;

    protected override void Init()
    {
        base.Init();

        Transform _tilesTrans = transform.Find(CEILS_NAME);
        _ceils = new Ceil[_height, _width];
        for (int z = 0; z < _height; ++z)
        {
            for (int x = 0; x < _width; ++x)
            {
                int index = z * _width + x;
                _ceils[z, x] = _tilesTrans.GetChild(index).GetComponent<Ceil>();
                _ceils[z, x].coordinate = new CeilCoordinate(z, x);
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

    public Ceil GetCeil(Vector3 pos)
    {
        Vector3 centerPos = GetCenterPosition(pos);
        return GetCeil((int) centerPos.z, (int) centerPos.x);
    }

    public Ceil GetCeil(int row, int col)
    {
        return NotValidateCoordinate(row, col) ? null : _ceils[row, col];
    }

    private bool NotValidateCoordinate(int row, int col)
    {
        return row < 0 || row >= _width || col < 0 || col > _height;
    }

    public bool IsEmptyTile(Vector3 pos)
    {
        return GetCeil(pos).items.Count == 0;
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
        foreach (CeilItem tileItem in Resources.LoadAll<CeilItem>(CEIL_ITEMS_FILE_NAME))
            _tileItemPrefabDic[tileItem.shorterName] = tileItem.gameObject;
    }

    private void GenrateTiles()
    {
        ClearTiles();

        GameObject tilesGO = new GameObject(CEILS_NAME);
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
                GameObject newCeilGO = new GameObject("Ceil");
                newCeilGO.transform.parent = tilesGO.transform;
                newCeilGO.transform.SetAsLastSibling();
                Vector3 centerPos = new Vector3(x, 0f, z);
                newCeilGO.transform.position = centerPos;

                Ceil newCeil = newCeilGO.AddComponent<Ceil>();
                newCeil.coordinate = new CeilCoordinate(z, x);

                string[] ceilItemNames = rowDatas[x].Split(new[] {"|"}, options);
                foreach (string ceilItemName in ceilItemNames)
                {
                    if (_tileItemPrefabDic.ContainsKey(ceilItemName))
                    {
                        CeilItem ceilItem =
                            Instantiate(_tileItemPrefabDic[ceilItemName], centerPos, Quaternion.identity)
                                .GetComponent<CeilItem>();
                        ceilItem.transform.parent = newCeilGO.transform;
                    }
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

        GameObject floorGO = new GameObject(FLOOR_NAME);
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
        Transform floor = transform.Find(CEILS_NAME);
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
        if (_ceils != null)
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
