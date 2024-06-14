using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected Vector3 direction;
    public Character Owner { get; set; }

   
    void Update()
    {
        transform.Translate(this.direction * this.moveSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character != Owner)
        {
            SimplePool.Despawn(this);
            // Destroy(character.gameObject);
            character.Die();
        }
    }
}
