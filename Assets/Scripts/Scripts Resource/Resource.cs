using System;
using UnityEngine;

public abstract class Resource: MonoBehaviour
{
    public event Action<Resource> Removed;

    public void Remove() => 
        Removed?.Invoke(this);

    public abstract void TurnOff();

    public abstract void TurnOn();
}
