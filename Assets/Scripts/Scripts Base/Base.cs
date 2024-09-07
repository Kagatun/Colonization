using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    private List<Resource> _resources = new List<Resource>();
    private float _repeatRate = 0.1f;

    private void Start()
    {
        AssignBaseBots();
        InvokeRepeating(nameof(ScanResources), 0.0f, _repeatRate);
    }

    private void ScanResources()
    {
        foreach (var resource in _resourceScanner.Scan())
        {
            if (resource != null && !_resources.Contains(resource))
            {
                _resources.Add(resource);
            }
        }

        Vector3 position = transform.position;

        _resources = _resources
            .Where(resource => resource != null && resource.IsReserved == false)
            .OrderBy(resource => Vector3.Distance(resource.transform.position, position))
            .ToList();

        SetTargetBot();
    }

    private void SetTargetBot()
    {
        if (_resources.Count != 0)
        {
            var availableResources = _resources.Where(resource => resource.IsReserved == false).ToList();
            var availableBots = _bots.Where(bot => bot.DesignatedResource == null && bot.IsBusy == false).ToList();
            var crystalsToRemove = new List<Resource>();

            foreach (var resource in availableResources)
            {
                var bot = availableBots.FirstOrDefault();

                if (bot != null)
                {
                    if (!CheckAssignedResource(resource))
                    {
                        bot.GetBusy();
                        bot.AssignResource(resource);
                        bot.GoToResource();
                        resource.GetReserve();
                        crystalsToRemove.Add(resource);
                        availableBots.Remove(bot);
                    }
                }
            }

            foreach (var resource in crystalsToRemove)
                _resources.Remove(resource);
        }
    }

    private bool CheckAssignedResource(Resource resource)
    {
        foreach (var bot in _bots)
        {
            if (bot.DesignatedResource == resource)
                return true;
        }

        return false;
    }

    private void AssignBaseBots()
    {
        foreach (var bot in _bots)
        {
            bot.AssignBase(this);
        }
    }
}
