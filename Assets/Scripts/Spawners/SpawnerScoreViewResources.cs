using UnityEngine;

public class SpawnerScoreViewResources : SpawnerObjects<ScoreViewResources>
{
    [SerializeField] private SpawnerBases _spawnerBases;
    [SerializeField] private Canvas _canvas;

    private void OnEnable()
    {
        _spawnerBases.BaseSpawned += OnSpawn;
    }

    private void OnDisable()
    {
        _spawnerBases.BaseSpawned -= OnSpawn;
    }

    private void OnSpawn(Base @base) =>
        SpawnScoreViewResources(@base);

    private ScoreViewResources SpawnScoreViewResources(Base @base)
    {
        float offsetX = -5.24f;
        float offsetY = 6f;
        float offsetZ = 0.74f;

        ScoreViewResources scoreViewResources = Get();
        scoreViewResources.AssignResourceWarehouse(@base.ResourceWarehouse);
        scoreViewResources.transform.position = @base.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        scoreViewResources.transform.SetParent(_canvas.transform);

        return scoreViewResources;
    }
}
