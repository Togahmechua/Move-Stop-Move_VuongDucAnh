using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData ins;
    public static GameData Ins => ins; 
    private int num;

    [SerializeField] private ColorsData colorsData;
    [SerializeField] private PantsData pantsData;
    [SerializeField] private HatsData hatsData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private ShieldData shieldData;
    [SerializeField] private SkinData skinData;


    public enum BodyPartType
    {
        None,
        Colors,
        Hat,
        Pants,
        Weapon,
        Shield,
        SetSkin,
    }
    
    [System.Serializable]
    public class SkinData
    {
        public BodyPartType bodyPartType;
        public SkinSO skinSO;

        public void SetSkin(Character character, int skinIndex )
        {
            FullSetItem existingSet = character.GetComponentInChildren<FullSetItem>();
            if (existingSet != null)
            {
                Destroy(existingSet.gameObject);
            }

            if (skinIndex >= 0 && skinIndex < skinSO.sets.Count)
            {
                FullSetItem newSet = Instantiate(skinSO.sets[skinIndex], character.transform);
                character.fullSetItem = newSet;
            }
        }
    }

    [System.Serializable]
    public class ColorsData
    {
        public BodyPartType bodyPartType;
        public ColorsSO colorsSO;

        public void RandomColorsForBots(Renderer body)
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

        public void SetPants(int pantEnum, Renderer pants)
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
        public HatsSO hatsSO;

        public void SetHats(int hatsEnum, Transform pos)
        {
            int hatsIndex = hatsEnum;
            EHats hatsType = (EHats)hatsIndex;
            GameObject newHat = hatsSO.GetHats(hatsType);

            // Remove existing hats if any
            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }

            Instantiate(newHat, pos);
        }
    }

    [System.Serializable]
    public class ShieldData
    {
        public BodyPartType bodyPartType;
        public ShieldSO shieldsSO;

        public void SetShield(int shieldsEnum, Transform pos)
        {
            int shieldsIndex = shieldsEnum;
            EShield shieldsType = (EShield)shieldsIndex;
            GameObject newShield = shieldsSO.GetShield(shieldsType);

            // Remove existing hats if any
            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }

            Instantiate(newShield, pos);
        }
    }

    [System.Serializable]
    public class WeaponData
    {
        public BodyPartType bodyPartType;
        public WeaponSO weaponSO;

        public int RandomWPNumber()
        {
            return Random.Range(0, System.Enum.GetValues(typeof(EWeapon)).Length);
        }

        public GameObject  SetWeaponsModelForBots(int weaponEnum, Transform pos)
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
        GameData.ins = this;
    }

    public void RandomSkinForBots(Renderer body, Renderer pants, Transform pos)
    {
        colorsData.RandomColorsForBots(body);
        pantsData.SetPants(Random.Range(0, System.Enum.GetValues(typeof(EMats)).Length), pants);
        hatsData.SetHats(Random.Range(0, System.Enum.GetValues(typeof(EHats)).Length), pos);
    }

    public GameObject RandomModelWeapon(int number,Transform pos)
    {
        return weaponData.SetWeaponsModelForBots(number, pos);
    }

    public Weapon RandomWeapon(int number)
    {
        return weaponData.SetWeapons(number);
    }
    #endregion

    #region Skin for Player
    public Weapon SetWeaponForPlayer(int wpNum)
    {   
        return weaponData.SetWeapons(wpNum);
    }

    public GameObject SetModelWeapon(int wpNum, Transform pos)
    {
        return weaponData.SetWeaponsModelForBots(wpNum, pos);
    }

    public void SetPantForPlayer(int pantNum, Renderer pants)
    {
        pantsData.SetPants(pantNum, pants);
    }

    public void SetHatForPlayer(int hatNum, Transform pos)
    {
        hatsData.SetHats(hatNum, pos);
    }

    public void SetShieldForPlayer(int shieldNum, Transform pos)
    {
        shieldData.SetShield(shieldNum, pos);
    }


    public void SetSkin(Character character, int skinIndex)
    {
        skinData.SetSkin(character, skinIndex);
    }
    #endregion
}

