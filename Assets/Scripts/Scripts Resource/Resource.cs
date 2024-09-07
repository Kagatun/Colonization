using System;
using UnityEngine;

public abstract class Resource: MonoBehaviour
{
    public event Action<Resource> Removed;

    public bool IsRaised { get; private set; }
    public bool IsReserved { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base @base))
            if (IsRaised == false)
                MoveAwayFromBase(@base.transform.position);
    }

    public void Remove() => Removed?.Invoke(this);

    public void MakeRaised() => IsRaised = true;

    public void MakeUnsupported() => IsRaised = false;

    public void GetReserve() => IsReserved = true;

    public void RemoveReserve() => IsReserved = false;

    private void MoveAwayFromBase(Vector3 basePosition)
    {
        float offsetDistance = 15f;
        float minRandom = 15f;
        float maxRandom = -15f;

        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(minRandom, maxRandom), 0, UnityEngine.Random.Range(minRandom, maxRandom)).normalized;
        transform.position = basePosition + randomDirection * offsetDistance;
    }
}
