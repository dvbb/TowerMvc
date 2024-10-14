using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBase : MonoBehaviour
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

    #region Components
    public Animator Anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        moveSpeed = defaultSpeed;

        gameObject.transform.position = LevelModel.Instance.waypoints.FirstOrDefault();
    }

    private void Update()
    {
        Walk();
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

    private void EnemyDead()
    {
        Reset();

        // Modify level data
        LevelModel.Instance.DestroyedEnemies++;
        GameModel.Instance.SubtractHealth(1);
    }

    private void EnemyReachCheckPoint()
    {
        EnemyDead();
        GameModel.Instance.SubtractHealth(1);
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = LevelModel.Instance.waypoints.FirstOrDefault();
        nextWaypoint = 0;
    }

    #endregion
}