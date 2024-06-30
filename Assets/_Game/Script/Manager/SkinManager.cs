using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : GameManager
{
    private static SkinManager ins;
    public static SkinManager Ins => ins;

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private HatSO hatSO;
    [SerializeField] private PantsSO pantsSO;
    private int num;

    private void Awake()
    {
        SkinManager.ins  = this;
        
    }

    public void SpawnWeapon()
    {
        // int weaponIndex = Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length); 
        int weaponIndex = 0;
        EWeapon weaponType = (EWeapon)weaponIndex;
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponSO.GetWeapon(weaponType));
    }
}
