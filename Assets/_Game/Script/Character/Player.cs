using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private Transform shootPos;

    private bool canShoot = true;

    private void FixedUpdate()
    {
        this.Move();
        Shoot();
    }

    protected override void Move()
    {
        base.Move();
        rb.velocity = new Vector3(joyStick.Horizontal * moveSpeed, rb.velocity.y, joyStick.Vertical * moveSpeed);

        if (joyStick.Horizontal != 0 || joyStick.Vertical != 0)
        {
            TF.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));   
        }
    }

    private void Shoot()
    {
        if (attackRange.isInRange && joyStick.Horizontal == 0 && joyStick.Vertical == 0 && canShoot)
        {
            StartCoroutine(ShootBulletDelay());
        }
    }

    private IEnumerator ShootBulletDelay()
    {
        canShoot = false;
        // Weapon newWeapon = Instantiate(weaponPrefab, shootPos.position, shootPos.rotation);
        Weapon newWeapon = SimplePool.Spawn<Weapon>(PoolType.Weapon, TF.position, TF.rotation);
        newWeapon.Owner = this;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
