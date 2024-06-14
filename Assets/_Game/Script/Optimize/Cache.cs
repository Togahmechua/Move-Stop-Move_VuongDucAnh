using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }


    private static Dictionary<Collider, Weapon> weapons = new Dictionary<Collider, Weapon>();

    public static Weapon GetWeapon(Collider collider)
    {
        if (!weapons.ContainsKey(collider))
        {
            weapons.Add(collider, collider.GetComponent<Weapon>());
        }

        return weapons[collider];
    }
    
}
