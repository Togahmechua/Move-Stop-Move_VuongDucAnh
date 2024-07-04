using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void OnFire(Transform pos, Character owner)
    {
        base.OnFire(pos,owner);
        Weapon hammer = SimplePool.Spawn<Hammer>(PoolType.Hammer, pos.position, pos.rotation);
        hammer.SetOwner(owner);
    }
}
