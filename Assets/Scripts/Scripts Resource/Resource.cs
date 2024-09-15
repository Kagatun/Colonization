using System;
using UnityEngine;

public abstract class Resource: MonoBehaviour
{
    public event Action<Resource> Removed;
    public event Action<Resource> Selected;

    public void Remove() => 
        Removed?.Invoke(this);

    public void SetParent(Transform transformParent)
    {
        transform.parent = transformParent;
        Selected?.Invoke(this);
    }

    public abstract void TurnOff();

    public abstract void TurnOn();
}
