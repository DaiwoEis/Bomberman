﻿using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] _components = null;

    private void Awake()
    {
        Role role = GetComponent<Role>();
        role.OnSpawn += EnableComponents;
        role.OnDeath += DisableComponents;
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
