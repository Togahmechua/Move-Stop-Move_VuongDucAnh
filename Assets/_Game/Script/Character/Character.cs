using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public delegate void CharacterDeathHandler(Character character);
    public event CharacterDeathHandler OnCharacterDeath;

    public virtual void Die()
    {
        // Kích hoạt sự kiện OnCharacterDeath
        if (OnCharacterDeath != null)
        {
            OnCharacterDeath(this);
        }
    }

    protected virtual void Move()
    {
        //For override
    }
}
