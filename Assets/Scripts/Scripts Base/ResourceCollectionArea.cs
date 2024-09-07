using UnityEngine;

public class ResourceCollectionArea : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            if (resource.IsRaised == true)
            {
                if (resource is Crystal)
                {
                    _counter.AddCrystal();
                }

                if (resource is Cube)
                {
                    _counter.AddCube();
                }

                resource.Remove();
            }
        }

        if (other.gameObject.TryGetComponent(out Bot bot))
        {
            if (bot.DesignatedResource != null)
            {
                bot.RemoveBusy();
                bot.RemoveDesignatedResource();
            }
        }
    }
}
