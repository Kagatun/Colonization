using UnityEngine;

public class TargetPursuer : MonoBehaviour
{
    [SerializeField] private Transform _transformTarget;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        if (_transformTarget != null)
        {
            transform.position = _transformTarget.transform.position + _offset;
        }
    }
}

