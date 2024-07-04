
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSkin : MonoBehaviour
{
    [SerializeField] private ColorsData colorsData;
    [SerializeField] private PantsData pantsData;
    [SerializeField] private HatsData hatsData;
    [SerializeField] private WeaponData weaponData;
    
    public enum BodyPartType
    {
        None,
        Colors,
        Hat,
        Pants,
        Weapon
    }

    [System.Serializable]
    public class ColorsData
    {
        public BodyPartType bodyPartType;
        public ColorsSO colorsSO;
        public Renderer body;

        public void RandomColorsForBots()
        {
            int colorsIndex = Random.Range(0, System.Enum.GetValues(typeof(Ecolor)).Length);
            Ecolor colorsType = (Ecolor)colorsIndex;
            Material newMaterial = colorsSO.GetColors(colorsType);

            Material[] newMaterials = new Material[body.materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = newMaterial;
            }
            body.materials = newMaterials;
        }
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
        private int test;

        public int RandomWPNumber()
        {
            return Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
        }

        public GameObject  RandomWeaponsModelForBots(int weaponEnum)
        {
            int weaponIndex = weaponEnum;
            EWeapon weaponType = (EWeapon)weaponIndex;
            GameObject newWeapon = weaponSO.GetWeaponsModel(weaponType);

            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }

            // Instantiate and attach the new hat
            GameObject instantiatedWeapon = Instantiate(newWeapon, pos);
            return instantiatedWeapon;
        }

        public Weapon RandomWeapons(int weaponEnum)
        {
            int weaponIndex = weaponEnum;
            // Debug.Log(weaponIndex);
            EWeapon weaponType = (EWeapon)weaponIndex;
            Weapon newWeapon = weaponSO.GetWeapons(weaponType);
            return newWeapon;
        }
    }

    private void Awake()
    {
        weaponData.RandomWPNumber();
    }

    public void RandomSkinForBots()
    {
        colorsData.RandomColorsForBots();
        pantsData.RandomPantsForBots();
        hatsData.RandomHatsForBots();
    }

    public GameObject RandomModelWeapon()
    {
        return weaponData.RandomWeaponsModelForBots(weaponData.RandomWPNumber());
    }

    public Weapon RandomWeapon()
    {
        return weaponData.RandomWeapons(weaponData.RandomWPNumber());
    }
}

