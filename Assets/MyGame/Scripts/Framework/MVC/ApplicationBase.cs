using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ApplicationBase<T> : MonoSingleton<T> where T : MonoSingleton<T>
{
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName)
    {
        MVC.SendEvent(eventName);
    }

    protected void SendEvent(string eventName, object obj)
    {
        MVC.SendEvent(eventName, obj);
    }
}