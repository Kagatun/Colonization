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
        _renderer.material.color = _colorGreen;
    }

    private void OnDisable()
    {
        _touchedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base _))
        {
            HandleObjectEnter(other.gameObject);
        }
        else if (other.gameObject.TryGetComponent(out Resource resource) && resource.transform.parent == null)
        {
            HandleObjectEnter(other.gameObject);
            resource.Selected += OnResourceSelected;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HandleObjectExit(other.gameObject);
    }

    private void HandleObjectEnter(GameObject obj)
    {
        _renderer.material.color = _colorRed;
        CanInstall = false;
        _touchedObjects.Add(obj);
    }

    private void HandleObjectExit(GameObject obj)
    {
        if (_touchedObjects.Remove(obj))
        {
            if (obj.TryGetComponent(out Resource resource))
            {
                resource.Selected -= OnResourceSelected;
            }

            if (_touchedObjects.Count == 0)
            {
                _renderer.material.color = _colorGreen;
                CanInstall = true;
            }
        }
    }

    private void OnResourceSelected(Resource resource)
    {
        HandleObjectExit(resource.gameObject);
    }
}