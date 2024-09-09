using System;
using UnityEngine;

public class ResourceWarehouse : MonoBehaviour
{
    private int _crystalCount = 0;
    private int _cubeCount = 0;

    public event Action<int> CrystalCountChanged;
    public event Action<int> CubeCountChanged;

    public void AddCrystal()
    {
        _crystalCount++;
        CrystalCountChanged?.Invoke(_crystalCount);
    }

    public void AddCube()
    {
        _cubeCount++;
        CubeCountChanged?.Invoke(_cubeCount);
    }
}
