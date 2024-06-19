using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class BotCtrl : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AttackRange attackRange;

    private void Update()
    {
        Shoot();
        Move();
    }

    protected override void Move()
    {
        base.Move();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    protected override void Shoot()
    {
        if (attackRange.isInRange && canShoot)
        {
            base.Shoot();
        }
    }
}
