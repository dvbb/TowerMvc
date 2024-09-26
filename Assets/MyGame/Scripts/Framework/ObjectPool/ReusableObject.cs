using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour, IReusable
{

    #region IReusable implementation
    public abstract void Spawn();
    public abstract void UnSpawn();
    #endregion

}
