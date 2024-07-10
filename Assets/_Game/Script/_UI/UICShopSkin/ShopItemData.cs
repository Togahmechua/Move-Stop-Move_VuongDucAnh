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
    
}
