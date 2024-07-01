
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSkin : MonoBehaviour
{
    [SerializeField] private PantsData pantsData;
    [SerializeField] private HatsData hatsData;
    [SerializeField] private WeaponData weaponData;

    public enum BodyPartType
    {
        None,
        Hat,
        Pants,
        Weapon
    }

    [System.Serializable]
    public class PantsData
    {
        public BodyPartType bodyPartType;
        public PantsSO pantsSO;
        public Renderer pants;

        public void RandomPantsForBots()
        {
            int pantsIndex = Random.Range(0, System.Enum.GetValues(typeof(EMats)).Length);
            EMats pantsType = (EMats)pantsIndex;
            Material newMaterial = pantsSO.GetMaterial(pantsType);

            Material[] newMaterials = new Material[pants.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = newMaterial;
            }
            pants.materials = newMaterials;
        }
    }


    [System.Serializable]
    public class HatsData
    {
        public BodyPartType bodyPartType;
        public Transform pos;
        public HatsSO hatsSO;

        public void RandomHatsForBots()
        {
            int hatsIndex = Random.Range(0, System.Enum.GetValues(typeof(EHats)).Length);
            EHats hatsType = (EHats)hatsIndex;
            GameObject newHat = hatsSO.GetHats(hatsType);

            // Remove existing hats if any
            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }

            // Instantiate and attach the new hat
            Instantiate(newHat, pos);
        }
    }

    [System.Serializable]
    public class WeaponData
    {
        public BodyPartType bodyPartType;
        public Transform pos;
        public WeaponSO weaponSO;


        public void RandomWeaponsForBots()
        {
            int weaponIndex = Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
            EWeapon weaponType = (EWeapon)weaponIndex;
            GameObject newWeapon = weaponSO.GetWeapons(weaponType);

            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }

            // Instantiate and attach the new hat
            Instantiate(newWeapon, pos);
        }
    }

    public void ChangePantsExternal()
    {
        pantsData.RandomPantsForBots();
    }

    public void ChangeHatsExternal()
    {
        hatsData.RandomHatsForBots();
    }

    public void ChangeWeaponExternal()
    {
        weaponData.RandomWeaponsForBots();
    }
}

