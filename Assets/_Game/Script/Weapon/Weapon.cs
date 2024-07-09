using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected float moveSpeed;
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

    public virtual void OnFire(Transform pos, Character owner)
    {
    //    Debug.Log("IsShooting");
    }

    public void SetOwner(Character owner)
    {
        this.Owner = owner;
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
            character.isded = true;
        }
    }
}
