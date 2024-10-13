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
    private Vector3 from => transform.position + new Vector3(0, -2, 0);
    private Vector3 to => LevelModel.Instance.waypoints[nextWaypoint];

    #region Components
    public Animator Anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        moveSpeed = defaultSpeed;


    }

    private void Update()
    {
        Walk();
        foreach (var t in LevelModel.Instance.waypoints)
        {
            Debug.Log(t);
        }
    }

    #region Method

    public void Walk()
    {
        if (nextWaypoint == LevelModel.Instance.waypoints.Count())
        {
            nextWaypoint = 0;
            return;
        }

        if (Vector3.Distance(from, to) < .1f)
        {
            nextWaypoint += 1;
        }

        gameObject.transform.position = Vector3.MoveTowards(from, to, moveSpeed * Time.deltaTime);
    }

    #endregion
}