using System.Collections;
using UnityEngine;

public class SpawnerResources : SpawnerObjects<Resource>
{
    [SerializeField] private float _repeatRate;

    private WaitForSecondsRealtime _wait;
    private Coroutine _spawnCoroutine;
    private float _minRandom = -210;
    private float _maxRandom = 210;

    private void Start()
    {
        _wait = new WaitForSecondsRealtime(_repeatRate);
        _spawnCoroutine = StartCoroutine(CreateRecourses());
    }

    protected override Resource CreateObject() =>
        base.CreateObject();

    protected override void OnGet(Resource resource)
    {
        resource.TurnOn();
        resource.Removed += OnRemoveObject;
        base.OnGet(resource);
    }

    protected override void OnRelease(Resource resource)
    {
        resource.transform.parent = null;
        resource.Removed -= OnRemoveObject;
        base.OnRelease(resource);
    }

    protected override void Destroy(Resource resource)
    {
        resource.Removed -= OnRemoveObject;
        base.Destroy(resource);
    }

    private Resource SpawnResource()
    {
        Base[] bases = GameObject.FindObjectsOfType<Base>();
        Beacon[] beacons = GameObject.FindObjectsOfType<Beacon>();

        Vector3 spawnPoint;

        do
        {
            spawnPoint = GetRandomSpawnPoint();

        } while (GetAllowedDistance(spawnPoint, bases, beacons) == false);

        Resource resource = Get();
        resource.transform.position = spawnPoint;
        resource.transform.rotation = Quaternion.Euler(0, Random.Range(_minRandom, _maxRandom), 0);

        return resource;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float spawnX = Random.Range(_minRandom, _maxRandom);
        float spawnZ = Random.Range(_minRandom, _maxRandom);

        return new Vector3(spawnX, 0, spawnZ);
    }

    private bool GetAllowedDistance(Vector3 spawnPoint, Base[] bases, Beacon[] beacons)
    {
        float minDistance = 10;

        foreach (Base baseObject in bases)
        {
            float distance = (spawnPoint - baseObject.transform.position).sqrMagnitude;

            if (distance < minDistance)
                return false;
        }

        foreach (Beacon beacon in beacons)
        {
            float distance = Vector3.Distance(spawnPoint, beacon.transform.position);

            if (distance < minDistance)
                return false;
        }

        return true;
    }

    private IEnumerator CreateRecourses()
    {
        while (enabled == true)
        {
            SpawnResource();

            yield return _wait;
        }
    }
}