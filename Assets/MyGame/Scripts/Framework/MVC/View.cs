using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }

    // Attention events list
    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();

    public abstract void HandleEvent(string eventName, object obj);

    protected Model GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>();
    }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }

    public virtual void RegisterEvents()
    {

    }
}