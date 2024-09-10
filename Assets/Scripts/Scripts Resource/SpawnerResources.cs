using System.Collections;
using UnityEngine;

public class SpawnerResources : SpawnerObjects<Resource>
{
    [SerializeField] private float _repeatRate;

    private WaitForSecondsRealtime _wait;
    private Coroutine _spawnCoroutine;
    private float _minRandom = -80;
    private float _maxRandom = 80;

    private void Start()
    {
        _wait = new WaitForSecondsRealtime(_repeatRate);
        _spawnCoroutine = StartCoroutine(CreateRecourses());
    }

    protected override Resource CreateObject() => base.CreateObject();

    protected override void OnGet(Resource resource)
    {
        resource.TurnOn();
        resource.Removed += RemoveObject;
        base.OnGet(resource);
    }

    protected override void OnRelease(Resource resource)
    {
        resource.transform.parent = null;
        resource.Removed -= RemoveObject;
        base.OnRelease(resource);
    }

    protected override void Destroy(Resource resource)
    {
        resource.Removed -= RemoveObject;
        base.Destroy(resource);
    }

    private Resource Spawn()
    {
        Base[] bases = GameObject.FindObjectsOfType<Base>();

        float spawnRotationX = UnityEngine.Random.Range(_minRandom, _maxRandom);
        float spawnPositionZ = UnityEngine.Random.Range(_minRandom, _maxRandom);
        float spawnRotationY = UnityEngine.Random.Range(_minRandom, _maxRandom);

        Vector3 spawnPoint = new Vector3(spawnRotationX, 0, spawnPositionZ);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnRotationY, 0);

        foreach (Base baseObject in bases)
            if (Vector3.Distance(spawnPoint, baseObject.transform.position) < 20)
                spawnPoint = GetNewSpawnPoint(baseObject.transform.position);

        Resource resource = GetPool().Get();

        resource.transform.position = spawnPoint;
        resource.transform.rotation = spawnRotation;

        return resource;
    }

    private Vector3 GetNewSpawnPoint(Vector3 basePosition)
    {
        float randomPosition = 20f;
        float newX = basePosition.x + Random.Range(-randomPosition, randomPosition);
        float newZ = basePosition.z + Random.Range(-randomPosition, randomPosition);

        return new Vector3(newX, 0, newZ).normalized;
    }

    private IEnumerator CreateRecourses()
    {
        while(enabled == true)
        {
            Spawn();

            yield return _wait;
        }
    }
}
