using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollController : MonoSingleton<ObjectPollController>
{
    Dictionary<string, SubPool> MyPools = new Dictionary<string, SubPool>();

    public GameObject Spawn(object obj)
    {
        string name = obj.ToString();

        SubPool subPool;
        if (!MyPools.ContainsKey(name))
        {
            // if current pool not contain, create new
            RegisterSubPool(obj);
        }

        subPool = MyPools[name];
        return subPool.Spawn();
    }

    public void UnSpawn(GameObject obj)
    {
        foreach (var subPool in MyPools.Values)
        {
            if (subPool.ContainObject(obj))
            {
                subPool.UnSpawn(obj);
                break;
            }
        }
    }

    public void UnSpawnAll(GameObject obj)
    {
        foreach (var subPool in MyPools.Values)
        {
            subPool.UnSpawnAll();
        }
    }

    private void RegisterSubPool(object obj)
    {
        // Create parent empty and game object prefab
        GameObject emptyGameObject = new GameObject(obj.ToString());
        GameObject prefab = ResourcesLoadTool.Instance.ResourceLoadObject<GameObject>(obj);

        // rergister subpool
        SubPool subPool = new SubPool(prefab, emptyGameObject.transform);
        MyPools.Add(subPool.Name, subPool);
    }
}
