using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Collector _collector;

    public Resource DesignatedResource { get; private set; }
    public Base DesignatedBase { get; private set; }

    public void AssignResource(Resource resource) => DesignatedResource = resource;

    public void RemoveDesignatedResource() => DesignatedResource = null;

    public void AssignBase(Base designatedBase) => DesignatedBase = designatedBase;

    public void GoToBase() => _mover.MovingTowardsGoal(DesignatedBase.transform);

    public void GoToResource() => _mover.MovingTowardsGoal(DesignatedResource.transform);
}
