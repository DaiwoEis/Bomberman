using System;
using UnityEngine;

public class Actor : MonoBehaviour 
{
    public event Action onSpawn;

    public event Action onDeath;

    public event Action onDestroy;

    public void TriggerOnSpawnEvent()
    {
        if (onSpawn != null) onSpawn();
    }

    public void TriggerOnDeathEvent()
    {
        if (onDeath != null) onDeath();
    }

    public void TriggerOnDestroyEvent()
    {
        if (onDestroy != null) onDestroy();
    }
}
