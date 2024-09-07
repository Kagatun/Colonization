using UnityEngine;

public class SpawnerResources : SpawnerObjects<Resource>
{
    [SerializeField] private float _repeatRate;

    private float _minRandom = -220;
    private float _maxRandom = 220;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.0f, _repeatRate);
    }

    protected override Resource CreateObject() => base.CreateObject();

    protected override void OnGet(Resource resource)
    {
        resource.TurnOn();
        resource.MakeUnsupported();
        resource.RemoveReserve();
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
        float spawnRotationX = Random.Range(_minRandom, _maxRandom);
        float spawnPositionZ = Random.Range(_minRandom, _maxRandom);
        float spawnRotationY = Random.Range(_minRandom, _maxRandom);

        Vector3 spawnPoint = new Vector3(spawnRotationX, 0, spawnPositionZ);
        Quaternion spawnRotation = Quaternion.Euler(0, spawnRotationY, 0);

        Resource resource = pool.Get();

        resource.transform.position = spawnPoint;
        resource.transform.rotation = spawnRotation;

        return resource;
    }
}
