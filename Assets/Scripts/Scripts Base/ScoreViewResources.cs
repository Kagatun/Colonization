using TMPro;
using UnityEngine;

public class ScoreViewResources : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TMP_Text _crystalText;
    [SerializeField] private TMP_Text _cubeText;

    private void OnEnable()
    {
        _counter.CrystalCountChanged += OnCrystalCountChanged;
        _counter.CubeCountChanged += OnCubeCountChanged;
    }

    private void OnDisable()
    {
        _counter.CrystalCountChanged -= OnCrystalCountChanged;
        _counter.CubeCountChanged -= OnCubeCountChanged;
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