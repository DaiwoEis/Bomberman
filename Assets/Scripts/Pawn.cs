using System;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public event Action OnSpawn;

    public event Action OnDeath;

    public void TriggerOnSpawnEvent()
    {
        if (OnSpawn != null) OnSpawn();
    }

    public void TriggerOnDeathEvent()
    {
        if (OnDeath != null) OnDeath();
    }

    public virtual void Death() { }
}