using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
      public override void OnFire(Transform pos, Character owner)
    {
        base.OnFire(pos,owner);
        Weapon axe = SimplePool.Spawn<Axe>(PoolType.Axe_0, pos.position, pos.rotation);
        axe.SetOwner(owner);
    }
}
