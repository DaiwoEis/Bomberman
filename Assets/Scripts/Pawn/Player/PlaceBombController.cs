using UnityEngine;

public class PlaceBombController : Function
{
    private BombBag _bombBag = null;

    protected override void Awake()
    {
        base.Awake();

        _bombBag = GetComponent<BombBag>();
    }
	
	protected override void OnUpdate () 
	{
        if (Input.GetButtonDown("Place"))
        {
            Map map = Singleton<Map>.instance;
            if (map.IsEmptyTile(transform.position))
            {
                GameObject bomb;
                _bombBag.GetBomb(map.GetCenterPosition(transform.position), Quaternion.identity, out bomb);
            }
        }
    }
}
