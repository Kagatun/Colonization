using System;
using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    private Beacon _designatedBeacon;

    public event Action<Beacon> BeaconCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Beacon beacon))
        {
            if (_designatedBeacon == beacon)
            {
                BeaconCollected?.Invoke(beacon);

                beacon.AllowMove();
                beacon.ProhibitInstallation();
                beacon.gameObject.SetActive(false);
            }
        }
    }

    public void SetDesignatedBeacon(Beacon beacon) =>
        _designatedBeacon = beacon;
}
