using System;
using UnityEngine;

public class ResourceWarehouse : MonoBehaviour
{
    [field: SerializeField] public int ResourceCount { get; private set; }

    public event Action<int> ResourceCountChanged;

    public void AddResource()
    {
        ResourceCount++;
        ResourceCountChanged?.Invoke(ResourceCount);
    }

    public void RemoveResource(int price)
    {
        ResourceCount -= price;
        ResourceCountChanged?.Invoke(ResourceCount);
    }
}
