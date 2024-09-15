using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseScrollWheel = "Mouse ScrollWheel";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private float _cameraSpeed = 100f;
    private float _cameraSpeedMouse = 500f;
    private float _zoomSpeed = 10f;
    private float _minZoom = 10f;
    private float _maxZoom = 100f;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);
        float zoom = Input.GetAxis(MouseScrollWheel);
        float mouseX = Input.GetAxis(MouseX);
        float mouseZ = Input.GetAxis(MouseY);

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * _cameraSpeed * Time.deltaTime, Space.World);

        Camera.main.fieldOfView -= zoom * _zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, _minZoom, _maxZoom);

        if (Input.GetMouseButton(2))
        {
            Vector3 dragMovement = new Vector3(-mouseX, 0, -mouseZ);
            transform.Translate(dragMovement * _cameraSpeedMouse * Time.deltaTime, Space.World);
        }
    }
}
