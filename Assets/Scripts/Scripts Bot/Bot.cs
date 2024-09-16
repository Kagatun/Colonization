using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Collector _collector;
    [SerializeField] private BuilderBase _builderBase;

    public event Action<Bot> Arrived;

    public Resource DesignatedResource { get; private set; }
    public Base DesignatedBase { get; private set; }
    public Beacon DesignatedBeacon { get; private set; }

    private void Start()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _builderBase.BeaconCollected += OnBeaconCollected;
    }

    private void OnDestroy()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _builderBase.BeaconCollected -= OnBeaconCollected;
    }

    public void AssignResource(Resource resource)
    {
        DesignatedResource = resource;
        _collector.SetDesignatedResource(resource);
    }

    public void RemoveDesignatedResource()
    {
        DesignatedResource = null;
        _collector.SetDesignatedResource(null);
    }

    public void AssignBeacon(Beacon beacon)
    {
        DesignatedBeacon = beacon;
        _builderBase.SetDesignatedBeacon(beacon);
    }

    public void AssignBase(Base designatedBase) =>
        DesignatedBase = designatedBase;

    public void RemoveDesignatedBase() =>
        DesignatedBase = null;

    public void GoToBeacon() =>
        _mover.GoToTarget(DesignatedBeacon.transform);

    public void GoToResource() =>
        _mover.GoToTarget(DesignatedResource.transform);

    private void OnResourceCollected(Resource resource)
    {
        if (DesignatedResource == resource)
            _mover.GoToTarget(DesignatedBase.transform);
    }

    private void RemoveDesignatedBeacon()
    {
        DesignatedBeacon = null;
        _builderBase.SetDesignatedBeacon(null);
    }

    private void OnBeaconCollected(Beacon beacon)
    {
        if (DesignatedBeacon == beacon)
        {
            Arrived?.Invoke(this);
            RemoveDesignatedBeacon();
        }
    }
}