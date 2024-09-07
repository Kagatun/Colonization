using UnityEngine;

public class Cube : Resource
{
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public override void TurnOff() => Animator.enabled = false;

    public override void TurnOn() => Animator.enabled = true;
}
