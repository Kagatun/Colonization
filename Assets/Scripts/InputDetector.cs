using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseScrollWheel = "Mouse ScrollWheel";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private int _mouseButtonLeft = 0;
    private int _mouseButtonRight = 1;
    private int _mouseButtonWheel = 2;

    public event Action<Vector3> Moved;
    public event Action<Vector3> Dragged;
    public event Action<float> Zoomed;
    public event Action LeftClicked;
    public event Action RightClicked;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        if (horizontal != 0 || vertical != 0)
            Moved?.Invoke(new Vector3(horizontal, 0, vertical));

        float zoom = Input.GetAxis(MouseScrollWheel);

        if (zoom != 0)
            Zoomed?.Invoke(zoom);

        if (Input.GetMouseButtonDown(_mouseButtonLeft))
            LeftClicked?.Invoke();

        if (Input.GetMouseButtonDown(_mouseButtonRight))
            RightClicked?.Invoke();

        if (Input.GetMouseButton(_mouseButtonWheel))
        {
            float mouseX = Input.GetAxis(MouseX);
            float mouseY = Input.GetAxis(MouseY);
            Dragged?.Invoke(new Vector3(-mouseX, 0, -mouseY));
        }
    }
}
