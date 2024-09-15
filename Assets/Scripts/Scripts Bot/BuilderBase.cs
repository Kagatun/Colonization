using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    [SerializeField] private Bot _bot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Beacon beacon))
        {
            if (_bot.DesignatedBeacon == beacon)
            {
                _bot.SpecifyArrivalBot();
                _bot.RemoveDesignatedBeacon();

                beacon.AllowMove();
                beacon.ProhibitInstallation();
                beacon.gameObject.SetActive(false);
            }
        }
    }
}
