using System;
using UnityEngine;

public class Actor : MonoBehaviour 
{
    public event Action OnShow;

    public event Action OnHide;

    public void TriggerOnShowEvent()
    {
        if (OnShow != null) OnShow();
    }

    public void TriggerOnHideEvent()
    {
        if (OnHide != null) OnHide();
    }
}
