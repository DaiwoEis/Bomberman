using UnityEngine;

public class DestroyBehaviour : MonoBehaviour
{
    public float _delay = 3f;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Destroy(gameObject, _delay);
    }
}
