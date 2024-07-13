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
            GameData.Ins.SetHatForPlayer(id, player.hatPos);
        }
        else
        {
            UICShopSkin.Ins.ShowBoughtItemInfo(id);
            GameData.Ins.SetHatForPlayer(id, player.hatPos);
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
}
