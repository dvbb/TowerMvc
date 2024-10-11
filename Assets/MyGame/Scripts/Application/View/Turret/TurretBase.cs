using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public  class TurretBase : View
{
    [Header("Attack info")]
    [SerializeField] protected float coldDown;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] protected float damage = 10;

    [Header("Upgrade info")]
    [SerializeField] public float level = 1;
    [SerializeField] protected float upgradeAttackRange = .3f;
    [SerializeField] protected float upgradeColdDown = .2f;
    [SerializeField] protected float upgradeDamage = 5;
    [SerializeField] public float totalValue;
    [SerializeField] public float upgradeCost = 50;

    //[SerializeField] public List<Enemy> EnemyTargets { get; private set; }

    protected float timer;

    public override string Name => "TurretBase";

    public override void HandleEvent(string eventName, object obj)
    {
    }


    #region Method

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion
}