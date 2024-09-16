using System.Collections.Generic;
using UnityEngine;

public class DatabaseResources : MonoBehaviour
{
    private List<Resource> _resourcesReserved = new List<Resource>();

    public IReadOnlyList<Resource> ResourcesReserved => _resourcesReserved.AsReadOnly();

    public void ReserveResource(Resource resource) =>
        _resourcesReserved.Add(resource);

    public void RemoveResource(Resource resource) =>
        _resourcesReserved.Remove(resource);
}
