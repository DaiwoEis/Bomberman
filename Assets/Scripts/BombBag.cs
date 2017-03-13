using UnityEngine;

public class BombBag : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab = null;

    private Role _role = null;

    private int _remainBombNum = 0;

    [SerializeField]
    private TextAsset _bombNumberLevel = null;

    private IntLevelMaper _bombNumberMaper = null;

    private void Awake()
    {
	    _role = GetComponent<Role>();

        _bombNumberMaper = new IntLevelMaper(_bombNumberLevel);
    }

    private void Start ()
    {

        _remainBombNum = _bombNumberMaper.GetData(_role.bombNumberLevel);
    }
	
	private void Update () 
	{
	
	}

    public bool GetBomb(Vector3 position, Quaternion rotation, out GameObject go)
    {
        if (_remainBombNum > 0)
        {
            _remainBombNum--;
            go = Instantiate(_bombPrefab, position, rotation);
            go.GetComponent<Bomb>().bag = this;
            go.GetComponent<Bomb>().placer = gameObject;
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
