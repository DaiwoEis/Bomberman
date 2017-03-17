using UnityEngine;

public class Wall : Actor 
{
	private void Awake() { }

    private void Start() { }

    private void OnEnable()
    {
        TriggerOnShowEvent();
    }

    private void OnDisable()
    {
        TriggerOnHideEvent();
    }
}
