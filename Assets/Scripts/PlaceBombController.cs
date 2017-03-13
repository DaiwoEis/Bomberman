using UnityEngine;

public class PlaceBombController : MonoBehaviour
{
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
	        GameObject bomb;
	        if (_bombBag.GetBomb(new Vector3(Mathf.RoundToInt(transform.position.x), 0f,
	                Mathf.RoundToInt(transform.position.z)),
	            Quaternion.identity, out bomb))
	        {
	            Debug.Log("Place bomb");
	        }
	        else
	        {
                Debug.Log("not have bomb");
            }
	    }
	}
}
