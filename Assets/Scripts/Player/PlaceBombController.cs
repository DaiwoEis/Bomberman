using UnityEngine;

public class PlaceBombController : MonoBehaviour
{
    [SerializeField]
    private Map _map = null;

    private BombBag _bombBag = null;

    private void Awake()
    {
        _bombBag = GetComponent<BombBag>();
    }

	private void Start () 
	{

	}
	
	private void Update () 
	{
        if (Input.GetButtonDown("Place"))
        {
            if (_map.IsEmptyTile(transform.position))
            {
                GameObject bomb;
                _bombBag.GetBomb(_map.GetCenterPosition(transform.position), Quaternion.identity, out bomb);
            }
        }
    }
}
