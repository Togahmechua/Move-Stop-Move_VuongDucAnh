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
                GameData.Ins.SetHatForPlayer(id, player.fullSetItem.hatPos);
                break;
            case EskinType.Pant:
                GameData.Ins.SetPantForPlayer(id, player.fullSetItem.PantRenderer);
                break;
            case EskinType.Shield:
                GameData.Ins.SetShieldForPlayer(id, player.fullSetItem.shieldPos);
                break;
            case EskinType.Skin:
                player.PlayerSetSkin(id);
                break;
        }
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
