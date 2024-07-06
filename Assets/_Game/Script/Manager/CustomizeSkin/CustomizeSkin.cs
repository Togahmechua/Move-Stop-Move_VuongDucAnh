
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSkin : MonoBehaviour
{
    [SerializeField] private ColorsData colorsData;
    [SerializeField] private PantsData pantsData;
    [SerializeField] private HatsData hatsData;
    [SerializeField] private WeaponData weaponData;
    private int num;
    
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

        public void SetPants(int pantEnum)
        {
            int pantsIndex = pantEnum;
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

        public void SetHats(int hatsEnum)
        {
            int hatsIndex = hatsEnum;
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

        public int RandomWPNumber()
        {
            return Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
        }

        public GameObject  SetWeaponsModelForBots(int weaponEnum)
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

        public Weapon SetWeapons(int weaponEnum)
        {
            int weaponIndex = weaponEnum;
            // Debug.Log(weaponIndex);
            EWeapon weaponType = (EWeapon)weaponIndex;
            Weapon newWeapon = weaponSO.GetWeapons(weaponType);
            return newWeapon;
        }
    }

    #region Random for Bots
    private void Awake()
    {
        num = weaponData.RandomWPNumber();
    }

    public void RandomSkinForBots()
    {
        colorsData.RandomColorsForBots();
        pantsData.SetPants(Random.Range(0, System.Enum.GetValues(typeof(EMats)).Length));
        hatsData.SetHats(Random.Range(0, System.Enum.GetValues(typeof(EHats)).Length));
    }

    public GameObject RandomModelWeapon()
    {
        return weaponData.SetWeaponsModelForBots(num);
    }

    public Weapon RandomWeapon()
    {
        return weaponData.SetWeapons(num);
    }
    #endregion

    #region Skin for Player
    public Weapon SetWeaponForPlayer(int wpNum)
    {   
        return weaponData.SetWeapons(wpNum);
    }

    public GameObject SetModelWeapon(int wpNum)
    {
        return weaponData.SetWeaponsModelForBots(wpNum);
    }

    public void SetPantForPlayer(int pantNum)
    {
        pantsData.SetPants(pantNum);
    }

    public void SetHatForPlayer(int hatNum)
    {
        hatsData.SetHats(hatNum);
    }
    #endregion
}

