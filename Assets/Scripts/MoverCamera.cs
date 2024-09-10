using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float _speed = 80f;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        Vector3 targetPosition = transform.position + movement * _speed * Time.deltaTime;
        targetPosition.y = Mathf.Max(transform.position.y, 0);

        transform.position = targetPosition;
    }
}
