using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour, IReusable
{

    #region IReusable implementation
    public abstract void Spawn();
    public abstract void UnSpawn();
    #endregion

}
