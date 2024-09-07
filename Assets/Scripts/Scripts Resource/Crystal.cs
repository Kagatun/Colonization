using UnityEngine;

[RequireComponent(typeof(Light))]
public class Crystal : Resource
{
    public Light Light { get; private set; }

    private void Awake()
    {
        Light = GetComponent<Light>();
    }

    public override void TurnOff() => Light.enabled = false;

    public override void TurnOn() => Light.enabled = true;
}
