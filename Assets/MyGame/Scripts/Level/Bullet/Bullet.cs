using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bullet : MonoBehaviour
{
    [Header("Bullet info")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float damage = 10;

    public Enemy Target { get; set; }

    public bool canMove;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (canMove)
            Move();
    }

    protected virtual void Move()
    {
        if (Target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, Target.transform.position) < .1)
        {
            HittedTarget();
        }
    }

    protected virtual void HittedTarget()
    {
        Target.TakeDamage(damage);
        RestBullet();
    }

    public void Init(Vector3 from, Vector3 to)
    {
        transform.position = from;
        Vector3 targetPosition = to - from;

        // Set rotation
        float angle = Vector3.SignedAngle(transform.up, targetPosition, transform.forward);
        transform.localEulerAngles = Vector3.zero;
        transform.Rotate(0, 0, angle);

    }

    public void RestBullet()
    {
        gameObject.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
        canMove = false;
    }
}