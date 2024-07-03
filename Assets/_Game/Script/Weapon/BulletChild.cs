using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChild : MonoBehaviour
{
    // [SerializeField] private WeaponData weaponData;

    // [System.Serializable]
    // public class WeaponData
    // {
    //     public Transform pos;
    //     public WeaponSO weaponSO;


    //     public Weapon RandomWeaponsForBots()
    //     {
    //         int weaponIndex = Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
    //         EWeapon weaponType = (EWeapon)weaponIndex;
    //         Weapon newWeapon = weaponSO.GetWeapons(weaponType);

    //         foreach (Transform child in pos)
    //         {
    //             Destroy(child.gameObject);
    //         }

    //         // Instantiate and attach the new hat
    //         Weapon instantiatedWeapon = Instantiate(newWeapon, pos);
    //         return instantiatedWeapon;
    //     }
    // }

    // public Weapon RandomWeapon()
    // {
    //     return weaponData.RandomWeaponsForBots();
    // }
}
