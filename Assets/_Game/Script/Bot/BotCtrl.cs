using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class BotCtrl : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    protected override void Move()
    {
        base.Move();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void SelfRemove()
    {

    }
}
