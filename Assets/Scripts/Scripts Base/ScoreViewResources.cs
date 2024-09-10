using TMPro;
using UnityEngine;

public class ScoreViewResources : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private TMP_Text _resourceText;

    private void OnEnable()
    {
        _resourceWarehouse.ResourceCountChanged += OResourceCountChanged;
    }

    private void OnDisable()
    {
        _resourceWarehouse.ResourceCountChanged -= OResourceCountChanged;
    }

    private void OResourceCountChanged(int count)
    {
        _resourceText.text = "Ресурсы: " + count.ToString();
    }
}