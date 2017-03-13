using UnityEngine;

public class FillMapWithInTime : MonoBehaviour
{
	private void Awake() { }

    private void Start()
    {
        GameObject.Find("MapData").GetComponent<Map>().FillMap(transform.position);
    }

    private void Update() { }

    private void OnExplosion()
    {
        GameObject.Find("MapData").GetComponent<Map>().UnFillMap(transform.position);
    }
}
