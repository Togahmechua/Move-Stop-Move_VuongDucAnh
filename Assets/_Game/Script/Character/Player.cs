using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float moveSpeed;



    private void Update()
    {
        if (Mathf.Abs(joyStick.Vertical) > 0.001f || Mathf.Abs(joyStick.Horizontal) > 0.001f)
        {
            this.Move();
            ChangeAnim(Constants.ANIM_RUNNING);
        }
        else
        {
            if (canShoot == false) return;
            ChangeAnim(Constants.ANIM_IDLE);
            rb.velocity = Vector3.zero;
        }
        
        Shoot();
    }

    protected override void Move()
    {
        base.Move();
        rb.velocity = new Vector3(joyStick.Horizontal * moveSpeed, rb.velocity.y, joyStick.Vertical * moveSpeed);
        if (Mathf.Abs(joyStick.Vertical) > 0.001f || Mathf.Abs(joyStick.Horizontal) > 0.001f)
        {
            TF.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));
        }
    }

    protected override void Shoot()
    {
        if (attackRange.isInRange && joyStick.Horizontal == 0 && joyStick.Vertical == 0 && canShoot)
        {
            base.Shoot();
        }
        else
        {
            canShoot = true;
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
