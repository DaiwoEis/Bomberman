using UnityEngine;

public class PawnLifeManager : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] _components = null;

    private void Awake()
    {
        Character character = GetComponent<Character>();
        character.OnSpawn += EnableComponents;
        character.OnDeath += DisableComponents;
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
