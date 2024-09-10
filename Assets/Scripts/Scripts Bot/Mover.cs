using UnityEngine;

public class Mover : MonoBehaviour
{
    private Transform _positionTarget;

    private void Update()
    {
        if (_positionTarget == null)
            return;

        GoToTarget(_positionTarget);
    }

    public void GoToTarget(Transform positionTarget)
    {
        _positionTarget = positionTarget;

        float speed = 24.0f;
        float flightAltitude = 0.5f;
        float distanceToTargetSqr = 0.01f;

        Vector3 direction = (_positionTarget.position - transform.position).normalized;
        direction.y = 0;

        if (direction.sqrMagnitude > distanceToTargetSqr)
        {
            direction.Normalize();
            transform.forward = direction;

            Vector3 newPosition = transform.position + transform.forward * speed * Time.deltaTime;
            newPosition.y = Mathf.Max(newPosition.y, flightAltitude);

            transform.position = newPosition;
        }
    }
}
