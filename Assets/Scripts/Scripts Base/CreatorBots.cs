using UnityEngine;

public class CreatorBots : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private ParticleSystem _particleSystemCreateBot;
    [SerializeField] private SpawnerBots _spawnerBots;
    [SerializeField] private Beacon _beacon;

    private int _priceBot = 3;
    private int _minQuantityBots = 2;

    private void Update()
    {
        if (_beacon.IsActivated == false || _base.Bots.Count < _minQuantityBots || _beacon.CanBeMoved == false)
            Spawn();
    }

    public void AssignSpawnerBots(SpawnerBots spawnerBots) =>
        _spawnerBots = spawnerBots;

    private void SpawnBot()
    {
        Bot bot = _spawnerBots.SpawnBot(_base);
        _base.AddBot(bot);
    }

    private void Spawn()
    {
        if (_resourceWarehouse.ResourceCount >= _priceBot)
        {
            SpawnBot();
            _resourceWarehouse.RemoveResource(_priceBot);
            _particleSystemCreateBot.Play();
        }
    }
}
