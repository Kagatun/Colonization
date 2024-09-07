using UnityEngine;

public class Cube : Resource , IToggleable
{
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void TurnOff() => Animator.enabled = false;

    public void TurnOn() => Animator.enabled = true;
}
