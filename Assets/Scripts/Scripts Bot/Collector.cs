using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private Resource _designatedResource;

    public event Action<Resource> ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            if (_designatedResource == resource)
            {
                ResourceCollected?.Invoke(resource);

                resource.transform.parent = transform;
                resource.transform.position = transform.position;
                resource.TurnOff();
            }
        }
    }

    public void SetDesignatedResource(Resource resource) =>
        _designatedResource = resource;
}
