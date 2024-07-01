using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private GameObject[] weaponsArr; 

    public GameObject GetWeapons(EWeapon index)
    {
        return weaponsArr[(int) index];
    }
}

public enum EWeapon
{
    Hammer = 0,
    Candy_0 = 1,
    Knife = 2,
    Candy_1 = 3,
    Bommerang = 4,
    Candy_4 = 5,
    Axe_0 = 6,
    Candy_2 = 7,
    Axe_1 = 8,
    Z = 9,
    Arrow = 10,
    Uzi = 11
}
