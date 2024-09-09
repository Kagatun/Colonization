using System;
using UnityEngine;

public abstract class Resource: MonoBehaviour
{
    public event Action<Resource> Removed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base @base))
            if (transform.parent == null)
                MoveSide(@base.transform.position);
    }

    public void Remove() => Removed?.Invoke(this);

    public abstract void TurnOff();

    public abstract void TurnOn();

    private void MoveSide(Vector3 basePosition)
    {
        float offsetDistance = 15f;
        float minRandom = 15f;
        float maxRandom = -15f;

        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(minRandom, maxRandom), 0, UnityEngine.Random.Range(minRandom, maxRandom)).normalized;
        transform.position = basePosition + randomDirection * offsetDistance;
    }
}
