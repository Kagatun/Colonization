using UnityEngine;

public class ResourceCollectionArea : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _ResourceWarehouse;
    [SerializeField] private DatabaseResources _databaseResources;
    [SerializeField] private Base _base;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bot bot))
        {
            if (bot.DesignatedBase == _base && bot.GetComponentInChildren<Resource>() != null)
            {
                Resource resource = bot.GetComponentInChildren<Resource>();

                bot.RemoveDesignatedResource();
                _databaseResources.RemoveResource(resource);
                _base.RemoveListResources(resource);
                resource.Remove();
                _ResourceWarehouse.AddResource();
            }
        }
    }
}
