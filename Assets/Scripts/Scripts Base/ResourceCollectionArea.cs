using UnityEngine;

public class ResourceCollectionArea : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _ResourceWarehouse;
    [SerializeField] private DatabaseResources _databaseResources;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            if (resource.transform.parent != null)
            {
                Bot parentBot = resource.GetComponentInParent<Bot>();

                resource.Remove();

                if (parentBot != null)
                {
                    parentBot.RemoveDesignatedResource();
                    _databaseResources.RemoveResource(resource);
                }

                if (resource is Crystal)
                {
                    _ResourceWarehouse.AddCrystal();
                }
                else if (resource is Cube)
                {
                    _ResourceWarehouse.AddCube();
                }
            }
        }
    }
}
