using System.Collections.Generic;
using UnityEngine;

public class LayoutBase : MonoBehaviour
{
    private Renderer _renderer;
    private List<GameObject> _touchedObjects = new List<GameObject>();
    private Color _colorGreen = new Color(0f, 1f, 0f, 0.5f);
    private Color _colorRed = new Color(1f, 0f, 0f, 0.5f);

    public bool CanInstall { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _colorRed;
    }

    private void Update()
    {
        if(_touchedObjects.Count > 0)
        {
            _renderer.material.color = _colorRed;
            CanInstall = false;
        }
        else
        {
            _renderer.material.color = _colorGreen;
            CanInstall = true;
        }
    }

    private void OnDisable()
    {
        _touchedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base @base))
        {
            _touchedObjects.Add(other.gameObject);
        }

        if (other.gameObject.TryGetComponent(out Resource resource) && resource.transform.parent == null)
        {
            _touchedObjects.Add(resource.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource) && resource.transform.parent != null)
        {
            _touchedObjects.Remove(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base @base))
        {
            _touchedObjects.Remove(@base.gameObject);
        }

        if (other.gameObject.TryGetComponent(out Resource resource) && resource.transform.parent == null)
        {
            _touchedObjects.Remove(resource.gameObject);
        }
    }
}