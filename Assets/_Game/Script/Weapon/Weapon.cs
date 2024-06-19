using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected Vector3 direction;
    [SerializeField] private float timeToDestroy;
    public Character Owner { get; set; }

    void Update()
    {
        StartCoroutine(DestroyByDistance());
        Move();
    }

    private IEnumerator DestroyByDistance()
    {
        yield return new WaitForSeconds(timeToDestroy);
        SimplePool.Despawn(this);
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
            SimplePool.Despawn(this);
            character.Die();
        }
    }

    
}
