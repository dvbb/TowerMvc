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

    protected float timer;
    public bool isSelected;

    public override string Name => "TurretBase";

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        AttentionEvents.Add(Consts.E_CardUnSelect);
    }

    public override void HandleEvent(string eventName, object obj)
    {
        switch (eventName)
        {
            case Consts.E_CardUnSelect:
                isSelected = false;
                AttackRangeShower.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = attackRange;
        AttackRangeShower.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 0);
    }

    #region Method
    public void Select()
    {
        isSelected = true;
        AttackRangeShower.SetActive(true);
        SendEvent(Consts.E_CardItemClick, card);
    }

    public void DisableSelect()
    {
        isSelected = false;
        AttackRangeShower.SetActive(false);
        SendEvent(Consts.E_CardUnSelect);
    }

    #endregion

    #region Unity Callback

    #endregion

    #region Event Callback

    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponent<Enemy>() != null)
        //{
        //    Enemy target = collision.GetComponent<Enemy>();
        //    EnemyTargets.Add(target);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.GetComponent<Enemy>() != null)
        //{
        //    Enemy target = collision.GetComponent<Enemy>();
        //    if (EnemyTargets.Contains(target))
        //        EnemyTargets.Remove(target);
        //}
    }
    #endregion
}