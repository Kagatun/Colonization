using TMPro;
using UnityEngine;

public class ScoreViewResources : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private TMP_Text _resourceText;

    private void OnEnable()
    {
        if(_resourceWarehouse!= null)
        _resourceWarehouse.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _resourceWarehouse.ResourceCountChanged -= OnResourceCountChanged;
    }

    public void AssignResourceWarehouse(ResourceWarehouse resourceWarehouse)
    {
        _resourceWarehouse = resourceWarehouse;
        _resourceWarehouse.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnResourceCountChanged(int count) =>
        _resourceText.text = "Ресурсы: " + count.ToString();
}