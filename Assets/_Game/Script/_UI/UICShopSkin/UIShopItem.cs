using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool IsBought => isBought; // Expose isBought property

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

        // Load purchase and equip status
        isBought = PlayerPrefs.GetInt("ItemBought_" + id, 0) == 1;
        GoLock.SetActive(!isBought);

        isEquip = PlayerPrefs.GetInt("ItemEquipped_" + id, 0) == 1;
        equippedText.SetActive(isEquip);
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
        
        // Save item purchase status
        PlayerPrefs.SetInt("ItemBought_" + id, 1);
        PlayerPrefs.Save();
    }

    public void SetEquip(bool equip)
    {
        isEquip = equip;
        equippedText.SetActive(equip);
        
        // Save item equip status
        PlayerPrefs.SetInt("ItemEquipped_" + id, equip ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void EquipItemToPlayer()
    {
        switch (DataConfig.eskinType)
        {
            case EskinType.Hat:
                GameData.Ins.SetHatForPlayer(id, player.fullSetItem.hatPos);
                break;
            case EskinType.Pant:
                GameData.Ins.SetPantForPlayer(id - 10, player.fullSetItem.PantRenderer);
                break;
            case EskinType.Shield:
                GameData.Ins.SetShieldForPlayer(id - 20, player.fullSetItem.shieldPos);
                break;
            case EskinType.SkinSet:
                player.PlayerSetSkin(id - 30);
                break;
        }
        
        // Save the current skin
        PlayerPrefs.SetInt("PlayerSkin", id);
        PlayerPrefs.Save();
    }

    private void BuffForPlayer()
    {
        switch (DataConfig.eBuffType)
        {
            case EBuffType.None:
                break;
            case EBuffType.AttackRange:
                break;
            case EBuffType.MoveSpeed:
                break;
            case EBuffType.Gold:
                break;
            case EBuffType.AttackSpeed:
                break;
        }
    }
}
