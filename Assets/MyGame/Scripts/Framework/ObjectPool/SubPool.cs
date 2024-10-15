using System;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    List<GameObject> mySubPool = new List<GameObject>();
    Transform parentTransform;
    GameObject m_Prefab;

    public string Name
    {
        get
        {
            return m_Prefab.name;
        }
    }

    public SubPool(GameObject prefab, Transform transform)
    {
        m_Prefab = prefab;
        parentTransform = transform;
    }

    public GameObject Spawn()
    {
        GameObject obj = null;
        foreach (var item in mySubPool)
        {
            if (!item.activeSelf)
            {
                obj = item;
                break;
            }
        }

        if (obj == null)
        {
            obj = GameObject.Instantiate(m_Prefab);
            obj.transform.parent = parentTransform;
            mySubPool.Add(obj);
        }
        obj.SetActive(true);

        IReusable reusable = obj.GetComponent<IReusable>();
        reusable?.Spawn();

        return obj;
    }

    public void UnSpawn(GameObject obj)
    {
        if (mySubPool.Contains(obj))
        {
            IReusable ir = obj.GetComponent<IReusable>();
            if (ir != null)
            {
                ir.UnSpawn();
            }
            obj.SetActive(false);
        }
    }

    public void UnSpawnAll()
    {
        foreach (var obj in mySubPool)
        {
            if (obj.activeSelf)
                UnSpawn(obj);
        }
    }

    public bool ContainObject(GameObject obj)
    {
        return mySubPool.Contains(obj);
    }
}
