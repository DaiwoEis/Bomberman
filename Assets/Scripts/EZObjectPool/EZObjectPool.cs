using UnityEngine;
using System.Collections.Generic;

namespace EZObjectPools
{
    [AddComponentMenu("EZ Object Pool/Object Pool")]
    public class EZObjectPool : MonoBehaviour
    {
        public static readonly string PoolContainerTag = "ObjectPools";

        private static Dictionary<string, EZObjectPool> _sharedPools = new Dictionary<string, EZObjectPool>();

        private static GameObject _marker;

        [SerializeField]
        private GameObject _template;
        
        [SerializeField]
        private string _poolName;

        private List<GameObject> _objectList = null;

        private List<GameObject> _availableObjects = null;

        [SerializeField]
        private bool _autoResize = true;

        [SerializeField]
        private int _poolSize = 100;

        [SerializeField]
        private bool _instantiateOnAwake = false;

        [SerializeField]
        private bool _shared = false;

        public static GameObject marker
        {
            get
            {
                if (_marker == null)
                {
                    GameObject m = GameObject.FindWithTag(PoolContainerTag);
                    if (m == null)
                    {
                        Debug.LogError("Please make your scene have a " + PoolContainerTag + " tag GameObject");
                    }
                    _marker = m;
                }
                return _marker;
            }
        }

        private void Awake()
        {
            if (_instantiateOnAwake)
            {
                _objectList = new List<GameObject>(_poolSize);
                _availableObjects = new List<GameObject>(_poolSize);
                InstantiatePool();
            }

            if (_shared)
            {
                _sharedPools.Add(_poolName, this);
            }
        }

        public static EZObjectPool CreateObjectPool(GameObject template, string name, int size = 50, bool autoResize = true, bool instantiateImmediate = true, bool shared = false)
        {
            if (shared)
            {
                if (_sharedPools.ContainsKey(name))
                    return _sharedPools[name];

                GameObject go = new GameObject(name);
                go.transform.parent = marker.transform;
                EZObjectPool pool = go.AddComponent<EZObjectPool>();
                pool._instantiateOnAwake = false;
                pool._template = template;
                pool._poolSize = size;
                pool._poolName = name;
                pool._autoResize = autoResize;
                pool._objectList = new List<GameObject>(size);
                pool._availableObjects = new List<GameObject>(size);

                _sharedPools.Add(name, pool);

                if (instantiateImmediate)
                    pool.InstantiatePool();

                return pool;
            }
            else
            {
                GameObject go = new GameObject(name);
                go.transform.parent = marker.transform;
                EZObjectPool pool = go.AddComponent<EZObjectPool>();
                pool._instantiateOnAwake = false;
                pool._template = template;
                pool._poolSize = size;
                pool._poolName = name;
                pool._autoResize = autoResize;
                pool._objectList = new List<GameObject>(size);
                pool._availableObjects = new List<GameObject>(size);

                if (instantiateImmediate)
                    pool.InstantiatePool();

                return pool;
            }
        }

        public static EZObjectPool CreateSharedPool(GameObject template, string name, int size = 50,
            bool autoResize = true, bool instantiateImmediate = true)
        {
            return CreateObjectPool(template, name, size, autoResize: autoResize, instantiateImmediate: instantiateImmediate, shared: true);
        }

        public static EZObjectPool GetPoolByName(string name)
        {
            GameObject poolGO = marker.transform.Find(name).gameObject;
            return poolGO == null ? null : poolGO.GetComponent<EZObjectPool>();
        }

        private void InstantiatePool()
        {
            if (_template == null)
            {
                Debug.LogError("EZ Object Pool: " + name + ": Template GameObject is null! Make sure you assigned a template either in the inspector or in your scripts.");
                return;
            }

            ClearPool();

            for (int i = 0; i < _poolSize; i++)
            {
                GameObject go = NewActiveObject();
                _objectList.Add(go);
                go.SetActive(false);
                AddToAvailableObjects(go);
            }
        }

        public bool TryGetNextObject(Vector3 pos, Quaternion rot, out GameObject go)
        {
            if (_objectList.Count == 0)
            {
                Debug.LogError("EZ Object Pool " + _poolName + ", the pool has not been instantiated but you are trying to retrieve an object!");
            }

            int lastIndex = _availableObjects.Count - 1;

            if (_availableObjects.Count > 0)
            {
                if (_availableObjects[lastIndex] == null)
                {
                    Debug.LogError("EZObjectPool " + _poolName + " has missing objects in its pool! Are you accidentally destroying any GameObjects retrieved from the pool?");
                    go = null;
                    return false;
                }

                go = _availableObjects[lastIndex];
                go.SetActive(true);
                go.transform.position = pos;
                go.transform.rotation = rot;
                foreach (PooledObject pooledObject in go.GetComponents<PooledObject>())
                    pooledObject.OnRetrieveFromPool();
                _availableObjects.RemoveAt(lastIndex);
                return true;
            }

            if (_autoResize)
            {
                GameObject newGO = NewActiveObject();
                newGO.transform.position = pos;
                newGO.transform.rotation = rot;
                _objectList.Add(newGO);
                go = newGO;
                return true;
            }

            Debug.LogError("The pool is full");
            go = null;
            return false;
        }

        public void TryGetNextObject(Vector3 pos, Quaternion rot)
        {
            GameObject go;
            TryGetNextObject(pos, rot, out go);
        }

        private GameObject NewActiveObject()
        {
            GameObject newGO = Instantiate(_template);
            newGO.transform.parent = transform;

            PooledObject[] pooledObjects = newGO.GetComponents<PooledObject>();
            if (pooledObjects.Length <= 0)
            {
                PooledObject pooledObject = newGO.AddComponent<PooledObject>();
                pooledObject.SetUp(this);
                pooledObject.OnInstatiateFromPool();
            }
            else
            {
                foreach (PooledObject pooledObject in pooledObjects)
                {
                    pooledObject.SetUp(this);
                    pooledObject.OnInstatiateFromPool();
                }
            }

            return newGO;
        }

        public void ClearPool()
        {
            foreach (GameObject go in _objectList)
            {
                Destroy(go);
            }

            _objectList.Clear();
            _availableObjects.Clear();
        }

        public static void OnSceneChanged()
        {
            _sharedPools.Clear();
            _marker = null;
        }

        public void AddToAvailableObjects(GameObject obj)
        {
            _availableObjects.Add(obj);
        }

        public int ActiveObjectCount()
        {
            return _objectList.Count - _availableObjects.Count;
        }

        public int AvailableObjectCount()
        {
            return _availableObjects.Count;
        }
    }
}