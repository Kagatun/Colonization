using TMPro;
using UnityEngine;

public class ScoreViewResources : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private TMP_Text _resourceText;

    private void OnEnable()
    {
        if(_resourceWarehouse!= null)
        _resourceWarehouse.ResourceCountChanged += OResourceCountChanged;
    }

    private void OnDisable()
    {
        _resourceWarehouse.ResourceCountChanged -= OResourceCountChanged;
    }

    public void AssignResourceWarehouse(ResourceWarehouse resourceWarehouse)
    {
        _resourceWarehouse = resourceWarehouse;
        _resourceWarehouse.ResourceCountChanged += OResourceCountChanged;
    }

    private void OResourceCountChanged(int count) =>
        _resourceText.text = "Ресурсы: " + count.ToString();
}