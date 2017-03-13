﻿using System.Linq;
using UnityEngine;

public class Body : MonoBehaviour , IHitable
{
    [SerializeField]
    private string[] _canHittedTags = null;

	private void Start() { }

    public bool CanHit(GameObject hitter)
    {
        return _canHittedTags.Any(hitter.CompareTag);
    }

    public void Hit()
    {
        GetComponent<Role>().TriggerOnDeath();        
    }
}
