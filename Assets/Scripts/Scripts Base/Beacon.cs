using UnityEngine;

public class Beacon : MonoBehaviour
{
    public bool IsActivated { get; private set; } = false;
    public bool CanBeMoved { get; private set; } = true;

    public void AllowInstallation() =>
        IsActivated = true;

    public void ProhibitInstallation() =>
        IsActivated = false;

    public void AllowMove() =>
        CanBeMoved = true;

    public void ProhibitMoving() =>
        CanBeMoved = false;
}
