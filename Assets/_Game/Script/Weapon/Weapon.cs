using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected Vector3 direction;
    [SerializeField] private float timeToDestroy;
    public bool isDestroy;
    public Character Owner { get; set; }

    private void OnEnable()
    {
        StartCoroutine(DestroyByTime());
    }

    void Update()
    {
        Move();
    }

    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(timeToDestroy);
        DestroyWeapon();
    }

    private void DestroyWeapon()
    {
        if (Owner != null)
        {
            Owner.canShoot = true;
        }

        SimplePool.Despawn(this);
        isDestroy = true;
    }

    private void Move()
    {
        transform.Translate(this.direction * this.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character != Owner)
        {
            if (Owner != null)
            {
                Owner.canShoot = true;
                Owner.BuffScale();
            }
            SimplePool.Despawn(this);
            character.Die();
        }
    }

    // private IEnumerator WaitASec()
    // {
    //     yield return new WaitForSeconds
    // }
}
