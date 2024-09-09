using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatabaseResources : MonoBehaviour
{
    private List<Resource> _resourcesReserved = new List<Resource>();

    private void Update()
    {
        print("В резерве " + _resourcesReserved.Count);
    }

    public void ReserveResource(Resource resource)
    {
        _resourcesReserved.Add(resource);
    }

    public void RemoveResource(Resource resource)
    {
        _resourcesReserved.Remove(resource);
    }

    public IEnumerable<Resource> GetFreeResource(IEnumerable<Resource> resources) =>
        resources.Where(resource => _resourcesReserved.Contains(resource) == false);
}
