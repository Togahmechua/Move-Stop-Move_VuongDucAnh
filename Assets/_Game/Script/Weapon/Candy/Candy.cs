using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Weapon
{
    public override void OnFire(Transform pos, Character owner)
    {
        base.OnFire(pos,owner);
        Weapon candy = SimplePool.Spawn<Candy>(PoolType.Candy_0, pos.position, pos.rotation);
        candy.SetOwner(owner);
    }
}
