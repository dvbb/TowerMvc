using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Transform demageTextPosition;

    [Header("Basic info")]
    [SerializeField] protected float defaultSpeed = 3f;
    public float moveSpeed;
    [SerializeField] public float maxHealth;
    public float currentHealth;
    [SerializeField] public int fallingCoin = 5;

    // Walk
    private int nextWaypoint;
    private Vector3 spritePivotOffset = new Vector3(0, .4f, 0);
    private Vector3 from => transform.position;
    private Vector3 to => LevelModel.Instance.waypoints[nextWaypoint] + spritePivotOffset;

    // Dead logic
    private bool isDying;

    #region Components
    public Animator Anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    private DissolveEffect fxController;
    #endregion

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        fxController = GetComponent<DissolveEffect>();

        moveSpeed = defaultSpeed;

        gameObject.transform.position = LevelModel.Instance.waypoints.FirstOrDefault();
    }

    private void Update()
    {
        Walk();

        // If enemy completely dissolves, then reset
        if (isDying && fxController.GetDissolveAmount() == 1)
        {
            Reset();
        }
    }

    #region Method

    public void Walk()
    {
        if (Vector3.Distance(from, to) < .1f)
        {
            nextWaypoint += 1;
        }

        // Reach last waypoint?
        if (nextWaypoint >= LevelModel.Instance.waypoints.Count())
        {
            EnemyReachCheckPoint();
            return;
        }

        gameObject.transform.position = Vector3.MoveTowards(from, to, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        // if enemy is already dead before bullet arrival, then do nothing
        if (isDying)
            return;

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            isDying = true;
            Dead();
        }
    }

    private void Dead()
    {
        moveSpeed = 0;
        fxController.StartDissolve();

        // Modify level data
        LevelModel.Instance.EnemyDestroyed();
    }

    private void EnemyReachCheckPoint()
    {
        gameObject.SetActive(false);
        LevelModel.Instance.EnemyDestroyed();
        GameModel.Instance.SubtractHealth(1);
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        moveSpeed = defaultSpeed;
        nextWaypoint = 0;
        isDying = false;
        fxController.ResetDissolveAmount();

        gameObject.transform.position = LevelModel.Instance.waypoints.FirstOrDefault();
        gameObject.SetActive(true);
    }

    #endregion
}