public class Wall : Actor 
{
	private void Awake() { }

    private void Start()
    {
        TriggerOnSpawnEvent();
    }

    private void OnDestroy()
    {
        TriggerOnDeathEvent();
        TriggerOnDestroyEvent();
    }
}
