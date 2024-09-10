using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private DatabaseResources _databaseResources;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    private Coroutine _scanCoroutine;
    private List<Resource> _resources = new List<Resource>();
    private WaitForSecondsRealtime _wait;
    private float _repeatRate = 0.1f;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_repeatRate);
    }

    private void Start()
    {
        AssignBaseBots();
        _scanCoroutine = StartCoroutine(ScanResourcesRepeatedly());
    }

    public void RemoveListResources(Resource resource) => _resources.Remove(resource);

    private void ScanResources()
    {
        FillWithNewResources();
        SetTargetBot();
    }

    private void FillWithNewResources()
    {
        foreach (var resource in _resourceScanner.GetFoundResources())
        {
            if (_resources.Contains(resource) == false)
            {
                _resources.Add(resource);
            }
        }
    }

    private void SetTargetBot()
    {
        var availableBots = _bots.Where(bot => bot.DesignatedResource == null).ToList();

        if (availableBots.Count == 0)
            return;

        var reservedResources = _databaseResources.GetListReserveResources();
        var freeResources = _resources.Where(freeResource => reservedResources.Contains(freeResource)==false).ToList();

        _resources = freeResources.OrderBy(resource => Vector3.Distance(resource.transform.position, transform.position)).ToList();

        if (_resources.Count == 0)
            return;

        int resourcesToAssign = Mathf.Min(availableBots.Count, _resources.Count);

        for (int i = 0; i < resourcesToAssign; i++)
        {
            availableBots[i].AssignResource(_resources[i]);
            availableBots[i].GoToResource();
            _databaseResources.ReserveResource(_resources[i]);
        }
    }

    private void AssignBaseBots()
    {
        foreach (var bot in _bots)
        {
            bot.AssignBase(this);
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
