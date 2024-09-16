using System;
using UnityEngine;

public class SpawnerBases : SpawnerObjects<Base>
{
    [SerializeField] private DatabaseResources _databaseResources;
    [SerializeField] private SpawnerBots _spawnerBots;

    public event Action <Base> BaseSpawned;

    private void OnEnable()
    {
        _spawnerBots.BotSpawned += OnSpawn;
    }

    private void OnDisable()
    {
        _spawnerBots.BotSpawned -= OnSpawn;
    }

    private void OnSpawn(SpawnerBots spawnerBots, Bot bot) =>
        SpawnBase(spawnerBots, bot);

    private Base SpawnBase(SpawnerBots spawnerBots, Bot bot)
    {
        float offsetX = -0.1f;
        float offsetY = 0.1f;
        float offsetZ = -1.86f;

        Base newBase = Get();
        newBase.AssignDatabaseResources(_databaseResources);
        newBase.AssignSpawnerBots(spawnerBots);
        newBase.AddBot(bot);
        newBase.transform.position = bot.DesignatedBeacon.transform.position + new Vector3(offsetX, offsetY, offsetZ);

        BaseSpawned?.Invoke(newBase);

        return newBase;
    }
}
