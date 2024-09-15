using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Collector _collector;

    public event Action<Bot> Arrived;

    public Resource DesignatedResource { get; private set; }
    public Base DesignatedBase { get; private set; }
    public Beacon DesignatedBeacon { get; private set; }

    public void AssignResource(Resource resource) =>
        DesignatedResource = resource;

    public void RemoveDesignatedResource() =>
        DesignatedResource = null;

    public void AssignBeacon(Beacon beacon) =>
        DesignatedBeacon = beacon;

    public void RemoveDesignatedBeacon() =>
        DesignatedBeacon = null;

    public void AssignBase(Base designatedBase) =>
        DesignatedBase = designatedBase;

    public void RemoveDesignatedBase() =>
        DesignatedBase = null;

    public void GoToBeacon() =>
        _mover.GoToTarget(DesignatedBeacon.transform);

    public void GoToBase() =>
        _mover.GoToTarget(DesignatedBase.transform);

    public void GoToResource() =>
        _mover.GoToTarget(DesignatedResource.transform);

    public void SpecifyArrivalBot() =>
        Arrived?.Invoke(this);
}
