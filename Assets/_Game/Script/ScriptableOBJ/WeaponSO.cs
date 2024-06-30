using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private Weapon[] weaponsARR; 

    public Weapon GetWeapon(EWeapon index)
    {
        return weaponsARR[(int) index];
    }
}

public enum EWeapon
{
    Hammer = 0,
    Candy = 1
}
