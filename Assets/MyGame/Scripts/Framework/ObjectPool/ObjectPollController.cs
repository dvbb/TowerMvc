using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollController : MonoSingleton<ObjectPollController>
{
    Dictionary<string, SubPool> MyPools = new Dictionary<string, SubPool>();

    public GameObject Spawn(string name)
    {
        SubPool subPool;
        if (!MyPools.ContainsKey(name))
        {
            // if current pool not contain, create new
            RegisterSubPool(name);
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

    private void RegisterSubPool(string name)
    {
        GameObject obj = Resources.Load(name) as GameObject;
        SubPool subPool = new SubPool(obj);

        MyPools.Add(subPool.Name, subPool);
    }
}
