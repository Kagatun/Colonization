using UnityEngine;

public class Mover : MonoBehaviour
{
    private Transform _positionTarget;
    private float _speed = 25;

    private void Update()
    {
        if (_positionTarget != null)
            GoToTarget(_positionTarget);
    }

    public void GoToTarget(Transform positionTarget)
    {
        _positionTarget = positionTarget;

        float flightAltitude = 0.5f;
        float distanceToTargetSqr = 0.01f;

        Vector3 direction = (_positionTarget.position - transform.position).normalized;
        direction.y = 0;

        if (direction.sqrMagnitude > distanceToTargetSqr)
        {
            direction.Normalize();
            transform.forward = direction;

            Vector3 newPosition = transform.position + transform.forward * _speed * Time.deltaTime;
            newPosition.y = Mathf.Max(newPosition.y, flightAltitude);

            transform.position = newPosition;
        }
    }
}
