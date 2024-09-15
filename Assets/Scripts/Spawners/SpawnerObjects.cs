using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerObjects<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true);
    }

    protected ObjectPool<T> GetPool() =>
        _pool;

    protected void ReleaseToPool(T obj) =>
        _pool.Release(obj);

    protected void OnRemoveObject(T obj) =>
        ReleaseToPool(obj);

    protected virtual T CreateObject() =>
        Instantiate(_prefab);

    protected virtual void OnGet(T obj) =>
        obj.gameObject.SetActive(true);

    protected virtual void OnRelease(T obj) =>
        obj.gameObject.SetActive(false);

    protected virtual void Destroy(T obj) =>
        Destroy(obj.gameObject);
}
