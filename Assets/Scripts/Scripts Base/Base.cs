using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private SenderBots _senderBots;
    [SerializeField] private CreatorBots _creatorBots;
    [SerializeField] private DatabaseResources _databaseResources;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private ResourceWarehouse _resourceWarehouse;
    [SerializeField] private Beacon _beacon;
    [SerializeField] private ResourceCollectionArea _resourceCollectionArea;

    private Coroutine _scanCoroutine;
    private WaitForSecondsRealtime _wait;
    private float _repeatRate = 0.1f;
    private List<Bot> _bots = new List<Bot>();
    private List<Resource> _resources = new List<Resource>();

    public IReadOnlyList<Bot> Bots => _bots.AsReadOnly();
    public ResourceWarehouse ResourceWarehouse => _resourceWarehouse;
    public Beacon Beacon => _beacon;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_repeatRate);
    }

    private void Start()
    {
        _scanCoroutine = StartCoroutine(ScanResourcesRepeatedly());
    }

    public void AssignSpawnerBots(SpawnerBots spawnerBots) =>
        _creatorBots.AssignSpawnerBots(spawnerBots);

    public void RemoveListResources(Resource resource) =>
        _resources.Remove(resource);

    public void AssignDatabaseResources(DatabaseResources databaseResources)
    {
        _databaseResources = databaseResources;
        _resourceCollectionArea.AssignDatabaseResources(_databaseResources);
    }

    public void AddBot(Bot newBot) =>
        _bots.Add(newBot);

    public void RemoveBot(Bot bot) =>
        _bots.Remove(bot);

    private void ScanResources()
    {
        FillWithNewResources();
        SetTargetBot();
    }

    private void FillWithNewResources()
    {
        _resources.Clear();

        foreach (var resource in _resourceScanner.GetFoundResources())
            _resources.Add(resource);
    }

    private void SetTargetBot()
    {
        var availableBots = _bots.Where(bot => bot.DesignatedResource == null && bot.DesignatedBeacon == null).ToList();

        if (availableBots.Count == 0)
            return;

        var reservedResources = _databaseResources.ResourcesReserved;
        var freeResources = _resources.Where(freeResource => reservedResources.Contains(freeResource) == false).ToList();

        _resources = freeResources.OrderBy(resource => (resource.transform.position - transform.position).sqrMagnitude).ToList();

        if (_resources.Count == 0)
            return;

        int resourcesToAssign = Mathf.Min(availableBots.Count, _resources.Count);

        for (int i = 0; i < resourcesToAssign; i++)
        {
            availableBots[i].AssignResource(_resources[i]);
            availableBots[i].GoToResource();
            availableBots[i].AssignBase(this);
            _databaseResources.ReserveResource(_resources[i]);
        }
    }

    private IEnumerator ScanResourcesRepeatedly()
    {
        while (enabled == true)
        {
            ScanResources();

            yield return _wait;
        }
    }
}
