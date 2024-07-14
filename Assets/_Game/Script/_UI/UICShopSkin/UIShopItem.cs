using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    private ShopItemDataConfig DataConfig;
    private bool isBought;
    private Player player;

    public Button BtnSelect;
    public Image ImgIcon;
    public GameObject GoLock;
    public GameObject equippedText;
    public int id;
    public bool isEquip { get; private set; }
    public bool IsBought => isBought; // Added to expose isBought property

    void Start()
    {
        BtnSelect.onClick.AddListener(OnClickSelectItem);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Setup(ShopItemDataConfig shopItemDataConfig) 
    {
        DataConfig = shopItemDataConfig;
        id = DataConfig.ID;
        ImgIcon.sprite = DataConfig.spriteIcon;
        GoLock.SetActive(!isBought);
        equippedText.SetActive(false);
    }

    private void OnClickSelectItem()
    {
        if (!isBought)
        {
            UICShopSkin.Ins.ShowItemInfo(id);
            EquipItemToPlayer();
        }
        else
        {
            UICShopSkin.Ins.ShowBoughtItemInfo(id);
            EquipItemToPlayer();
        }
    }

    public void SetBought()
    {
        isBought = true;
        GoLock.SetActive(false);
    }

    public void SetEquip(bool equip)
    {
        isEquip = equip;
        equippedText.SetActive(equip);
    }

     private void EquipItemToPlayer()
    {
        switch (DataConfig.eskinType)
        {
            case EskinType.Hat:
                GameData.Ins.SetHatForPlayer(id, player.hatPos);
                break;
            case EskinType.Pant:
                GameData.Ins.SetPantForPlayer(id, player.pants);
                break;
            case EskinType.Shield:
                GameData.Ins.SetShieldForPlayer(id, player.shieldPos);
                break;
            case EskinType.Skin:
                GameData.Ins.SetSkinForPlayer(id, player.body, player.pants, player.hatPos, player.wingPos, player.tailPos, player.shieldPos);
                break;
        }
    }
}
