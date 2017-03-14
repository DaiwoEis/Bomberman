using UnityEngine;

public class FillMapWithInTime : MonoBehaviour
{
	private void Awake() { }

    private void OnEnable()
    {
        GameObject.Find("Map").GetComponent<Map>().FillMap(transform.position);
    }

    private void Update() { }

    private void OnDisable()
    {
        GameObject.Find("Map").GetComponent<Map>().UnFillMap(transform.position);
    }
}
