using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Bot _bot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            if (_bot.DesignatedResource == resource)
            {
                resource.MakeRaised();
                resource.transform.position = transform.position;
                resource.transform.parent = transform;
                resource.TurnOff();
                _bot.GoToBase();
            }
        }
    }
}
