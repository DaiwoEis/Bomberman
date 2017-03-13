using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float Delay = 3f;

    private void Start()
    {
        Destroy(gameObject, Delay);
    }
}
