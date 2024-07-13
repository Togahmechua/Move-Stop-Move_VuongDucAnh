using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICPantSkin : MonoBehaviour
{
    [SerializeField] private ShopItemData shopItemDataSO;
    [SerializeField] private UIShopItem UIShopItemPrefab;
    [SerializeField] private Transform pos;

    void Start()
    {
        for (int i = 0; i < shopItemDataSO.dataConfigs.Count; i++)
        {
            UIShopItem uiItem = Instantiate(UIShopItemPrefab, pos);
            uiItem.Setup(shopItemDataSO.dataConfigs[i]);
        }
    }
}
