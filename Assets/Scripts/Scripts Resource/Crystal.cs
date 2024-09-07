using UnityEngine;

[RequireComponent(typeof(Light))]
public class Crystal : Resource , IToggleable
{
    public Light Light { get; private set; }

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    public void TurnOff() => Light.enabled = false;

    public void TurnOn() => Light.enabled = true;
}
