using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    private static object mutext = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (mutext)
                {
                    if (_instance == null)
                        _instance = new T();
                }
            }
            return _instance;
        }
    }
}
