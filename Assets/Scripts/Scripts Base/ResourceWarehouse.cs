using System;
using UnityEngine;

public class ResourceWarehouse : MonoBehaviour
{
    private int _resourceCount = 0;

    public event Action<int> ResourceCountChanged;

    public void AddResource()
    {
        _resourceCount++;
        ResourceCountChanged?.Invoke(_resourceCount);
    }
}
