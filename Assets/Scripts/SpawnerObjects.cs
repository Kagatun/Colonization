using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerObjects<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;

    protected ObjectPool<T> pool;

    private void Awake()
    {
        pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true);
    }

    protected void ReleaseToPool(T obj) => pool.Release(obj);

    protected void RemoveObject(T obj) => ReleaseToPool(obj);

    protected virtual T CreateObject() => Instantiate(prefab);

    protected virtual void OnGet(T obj) => obj.gameObject.SetActive(true);

    protected virtual void OnRelease(T obj) => obj.gameObject.SetActive(false);

    protected virtual void Destroy(T obj) => Destroy(obj.gameObject);
}
