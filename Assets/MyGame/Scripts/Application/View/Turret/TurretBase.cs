using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TurretBase : View
{
    public Card card;

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

    // Component
    [SerializeField] private GameObject AttackRangeShower;

    // Battle info
    public List<EnemyBase> EnemyTargets = new List<EnemyBase>();
    protected float timer;
    public bool isSelected;

    public override string Name => "TurretBase";

    public override void HandleEvent(string eventName, object obj)
    {
    }

    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = attackRange;
        AttackRangeShower.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 0);

        // Init
        EnemyTargets.Clear();
    }

    private void Update()
    {
        Debug.Log(EnemyTargets.Count);
    }

    #region Method
    public void Select()
    {
        isSelected = true;
        AttackRangeShower.SetActive(true);
        SendEvent(Consts.E_CardUnSelect);
        SendEvent(Consts.E_HideNode);

        SendEvent(Consts.E_CardItemClick, card);
    }

    public void DisableSelect()
    {
        isSelected = false;
        AttackRangeShower.SetActive(false);
    }

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBase>() != null)
        {
            EnemyBase target = collision.GetComponent<EnemyBase>();
            EnemyTargets.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBase>() != null)
        {
            EnemyBase target = collision.GetComponent<EnemyBase>();
            if (EnemyTargets.Contains(target))
                EnemyTargets.Remove(target);
        }
    }
    #endregion
}