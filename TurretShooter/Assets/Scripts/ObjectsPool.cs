using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ObjectsPool
{
    [SerializeField] private List<GameObject> poolPrefabs;

    private Dictionary<PoolObjectId, IPoolObject> idToPrefab;
    private Dictionary<PoolObjectId, List<IPoolObject>> pools;

    public void Setup(int prewarmCount)
    {
        InitializeIdToPrefabDictionary();

        pools = new Dictionary<PoolObjectId, List<IPoolObject>>(poolPrefabs.Count);

        foreach (var prefabInterface in idToPrefab.Values)
        {
            var pool = new List<IPoolObject>(prewarmCount);

            for (int j = 0; j < prewarmCount; j++)
                pool.Add(Instantiate(prefabInterface));

            pools[prefabInterface.PoolId] = pool;
        }
    }

    public T GetObject<T>(PoolObjectId id)
    {
        var pool = pools[id];
        IPoolObject poolObject;

        if (pool.Count == 0)
        {
            var prefabInterface = idToPrefab[id];
            poolObject = Instantiate(prefabInterface);
        }
        else
        {
            poolObject = pool[0];
            pool.RemoveAt(0);
        }

        poolObject.Activate();
        return (T)poolObject;
    }

    public void ReleaseObject(IPoolObject poolObject)
    {
        pools[poolObject.PoolId].Add(poolObject);
        poolObject.Deactivate();
    }

    private IPoolObject Instantiate(IPoolObject prefabInterface)
    {
        var prefab = (MonoBehaviour)prefabInterface;

        var newObject = (IPoolObject)Object.Instantiate(prefab);
        newObject.Deactivate();

        return newObject;
    }

    private void InitializeIdToPrefabDictionary()
    {
        idToPrefab = new Dictionary<PoolObjectId, IPoolObject>(poolPrefabs.Count);
        foreach (var obj in poolPrefabs)
        {
            var poolObjectComponent = obj.GetComponent<IPoolObject>();
            if (poolObjectComponent == null)
                throw new Exception($"PoolPrefab is not a IPoolObject! {obj}");

            idToPrefab[poolObjectComponent.PoolId] = poolObjectComponent;
        }
    }
}