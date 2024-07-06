using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    public GameObject[] weaponModels;
    [SerializeField] private bool enableToNext = true;
    [SerializeField] private bool enableToBack = true;

    void Start()
    {
        foreach (GameObject weapon in weaponModels)
        {
            weapon.SetActive(false);
        }

        weaponModels[currentWeaponIndex].SetActive(true);
    }

    public void Update()
    {
        if (currentWeaponIndex >= weaponModels.Length - 1)
        {
            enableToNext = false;
        }
        else if (currentWeaponIndex <= 0)
        {
            enableToBack = false;
        }
        else
        {
            enableToNext = true;
            enableToBack = true;
        }
    }

    public void NextButton()
    {
        if (!enableToNext) return;
        weaponModels[currentWeaponIndex].SetActive(false);

        currentWeaponIndex++;
        weaponModels[currentWeaponIndex].SetActive(true);
    }

    public void PreviousButton()
    {
        if (!enableToBack) return;
        weaponModels[currentWeaponIndex].SetActive(false);

        currentWeaponIndex--;
        weaponModels[currentWeaponIndex].SetActive(true);
    }
}
