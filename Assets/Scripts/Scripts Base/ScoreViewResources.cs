using TMPro;
using UnityEngine;

public class ScoreViewResources : MonoBehaviour
{
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private TMP_Text _crystalText;
    [SerializeField] private TMP_Text _cubeText;

    private void OnEnable()
    {
        _resourceWarehouse.CrystalCountChanged += OnCrystalCountChanged;
        _resourceWarehouse.CubeCountChanged += OnCubeCountChanged;
    }

    private void OnDisable()
    {
        _resourceWarehouse.CrystalCountChanged -= OnCrystalCountChanged;
        _resourceWarehouse.CubeCountChanged -= OnCubeCountChanged;
    }

    private void OnCrystalCountChanged(int count)
    {
        _crystalText.text = "Кристаллы: " + count.ToString();
    }

    private void OnCubeCountChanged(int count)
    {
        _cubeText.text = "Кубы: " + count.ToString();
    }
}