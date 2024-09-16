using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;

    private float _cameraSpeed = 100f;
    private float _cameraSpeedMouse = 500f;
    private float _zoomSpeed = 10f;
    private float _minZoom = 10f;
    private float _maxZoom = 100f;

    private void OnEnable()
    {
        _inputDetector.Moved += OnMoveCamera;
        _inputDetector.Dragged += OnDragCamera;
        _inputDetector.Zoomed += OnZoomCamera;
    }

    private void OnDisable()
    {
        _inputDetector.Moved -= OnMoveCamera;
        _inputDetector.Dragged -= OnDragCamera;
        _inputDetector.Zoomed -= OnZoomCamera;
    }

    private void OnMoveCamera(Vector3 direction) =>
        transform.Translate(direction * _cameraSpeed * Time.deltaTime, Space.World);

    private void OnDragCamera(Vector3 dragDirection) =>
        transform.Translate(dragDirection * _cameraSpeedMouse * Time.deltaTime, Space.World);

    private void OnZoomCamera(float zoomAmount)
    {
        Camera.main.fieldOfView -= zoomAmount * _zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, _minZoom, _maxZoom);
    }
}