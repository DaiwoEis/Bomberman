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
	        Vector3 placePos = new Vector3(Mathf.RoundToInt(transform.position.x), 0f,
	            Mathf.RoundToInt(transform.position.z));
	        if (_map.CanPlace(placePos))
	        {
                GameObject bomb;
	            _bombBag.GetBomb(placePos, Quaternion.identity, out bomb);
	        }
	    }
	}
}
