using UnityEngine;

public class SpawnerScoreViewResources : SpawnerObjects<ScoreViewResources>
{
    [SerializeField] private SpawnerBases _spawnerBases;
    [SerializeField] private Canvas _canvas;

    private void OnEnable()
    {
        _spawnerBases.BaseSpawned += Spawn;
    }

    private void OnDisable()
    {
        _spawnerBases.BaseSpawned -= Spawn;
    }

    private ScoreViewResources SpawnScoreViewResources(Base @base)
    {
        float offsetX = -5.24f;
        float offsetY = 6f;
        float offsetZ = 0.74f;

        ScoreViewResources scoreViewResources = GetPool().Get();
        scoreViewResources.AssignResourceWarehouse(@base.ResourceWarehouse);
        scoreViewResources.transform.position = @base.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        scoreViewResources.transform.SetParent(_canvas.transform);

        return scoreViewResources;
    }

    private void Spawn(Base @base) =>
        SpawnScoreViewResources(@base);
}
