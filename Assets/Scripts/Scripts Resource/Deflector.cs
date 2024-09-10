using UnityEngine;

public class Deflector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base @base))
            if (transform.parent == null)
                MoveSide(@base.transform.position);
    }

    private void MoveSide(Vector3 basePosition)
    {
        float offsetDistance = 20;
        float minRandom = 20;
        float maxRandom = -20f;

        Vector3 randomDirection = new Vector3(Random.Range(minRandom, maxRandom), 0, Random.Range(minRandom, maxRandom)).normalized;
        transform.position = basePosition + randomDirection * offsetDistance;
    }
}
