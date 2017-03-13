using UnityEngine;

public class BombBag : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab = null;

    private Role _role = null;

    private int _bombNumberLevel = 0;

    private int bombNumberLevel
    {
        get { return _bombNumberLevel; }
        set
        {
            if (value > _bombNumberLevel)
            {
                _remainBombNum += _role.bombNumber - _maxBombNumber;
                _maxBombNumber = _role.bombNumber;
            }
            _bombNumberLevel = value;
        }
    }

    private int _maxBombNumber = 0;

    private int _remainBombNum = 0;


    private void Awake()
    {
	    _role = GetComponent<Role>();
    }

    private void Start ()
    {
        _remainBombNum = _maxBombNumber = _role.bombNumber;
    }
	
	private void Update ()
	{
	    bombNumberLevel = _role.bombNumberLevel;
	}

    public bool GetBomb(Vector3 position, Quaternion rotation, out GameObject go)
    {
        if (_remainBombNum > 0)
        {
            _remainBombNum--;
            go = Instantiate(_bombPrefab, position, rotation);
            Bomb bomb = go.GetComponent<Bomb>();
            bomb.bag = this;
            bomb.placer = gameObject;
            bomb.power = _role.bombPower;
            return true;
        }

        go = null;
        return false;
    }

    public void ReturnBag()
    {
        _remainBombNum++;
    }
}
