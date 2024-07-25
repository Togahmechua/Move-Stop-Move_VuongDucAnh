using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "ScriptableObjects/ShopItemData", order = 1)]
public class ShopItemData : ScriptableObject
{
    public List<ShopItemDataConfig> dataConfigs;
}

[Serializable]
public class ShopItemDataConfig {
    public int ID;
    public Sprite spriteIcon;
    public float Price;
    public bool isLock = true;
    public bool isEquip;
    public bool isUnequip;
    public EskinType eskinType;
    public EBuffType eBuffType;
}

public enum EskinType
{
    Hat,
    Pant,
    Shield,
    SkinSet
}

public enum EBuffType
{
    None = 0,
    AttackRange = 1,
    MoveSpeed = 2,
    Gold = 3,
    AttackSpeed = 4
}
