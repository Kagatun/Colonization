using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private DatabaseResources _databaseResources;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    private List<Resource> _resources = new List<Resource>();
    private float _repeatRate = 0.1f;
    private Coroutine _scanCoroutine;

    private void Start()
    {
        AssignBaseBots();
        _scanCoroutine = StartCoroutine(ScanResourcesPeriodically());
    }

    private void ScanResources()
    {
        FillWithNewResources();
        print("доступные" + _resources.Count);
        Vector3 position = transform.position;
        _resources = _resources.OrderBy(resource => Vector3.Distance(resource.transform.position, position)).ToList();

        SetTargetBot();
    }

    private void FillWithNewResources()
    {
        foreach (var resource in _resourceScanner.Scan())
        {
            if (resource != null && _resources.Contains(resource) == false)
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

        var freeResources = _databaseResources.GetFreeResource(_resources).ToList();

        foreach (var resource in freeResources)
        {
            if (availableBots.Count == 0)
                break;

            var bot = availableBots.FirstOrDefault();

            bot.AssignResource(resource);
            bot.GoToResource();

            _databaseResources.ReserveResource(resource);
            _resources.Remove(resource);

            availableBots.Remove(bot);
        }
    }

    private void AssignBaseBots()
    {
        foreach (var bot in _bots)
        {
            bot.AssignBase(this);
        }
    }

    private IEnumerator ScanResourcesPeriodically()
    {
        while (enabled == true)
        {
            ScanResources();

            yield return new WaitForSeconds(_repeatRate);
        }
    }
}
