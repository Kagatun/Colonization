using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskCrystal;

    private float radius = 500;

    public IEnumerable <Resource> Scan()
    {
        List<Resource> resources = new List<Resource>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, _layerMaskCrystal);

        foreach (Collider collider in colliders)
            if (collider.TryGetComponent(out Resource resource))
                resources.Add(resource);

        return resources;
    }
}
