using UnityEngine;

public class PawnLifeController : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] _components = null;

    private void Awake()
    {
        Pawn pawn = GetComponent<Pawn>();
        pawn.OnSpawn += EnableComponents;
        pawn.OnDeath += DisableComponents;
    }

    private void Start() { }

    private void EnableComponents()
    {
        foreach (var component in _components)
        {
            component.enabled = true;
        }
    }

    private void DisableComponents()
    {
        foreach (var component in _components)
        {
            component.enabled = false;
        }
    }
}
