using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    private ShopItemDataConfig DataConfig;
    private Player player;

    public Button BtnSelect;
    public Image ImgIcon;
    public Image TouchScreenSpr;
    public GameObject GoLock;
    public GameObject equippedText;
    public int id;
    public bool isEquip { get; private set; }
    public bool IsBought => DataConfig != null && DataConfig.isBought;

    private static UIShopItem currentlySelectedItem;
    private List<int> intList = new List<int>(); // List to keep track of equipped item IDs

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>(); 
        BtnSelect.onClick.AddListener(OnClickSelectItem);
        OnInit();
    }

    public void Update()
    {
        UpdateItemState();
    }

    public void OnInit()
    {
        TouchScreenSpr.gameObject.SetActive(false);
        GoLock.SetActive(true);
        equippedText.SetActive(false);
    }

    public void OnClick()
    {
        TouchScreenSpr.gameObject.SetActive(true);
        GoLock.SetActive(true);
        equippedText.SetActive(false);
    }

    public void Setup(ShopItemDataConfig shopItemDataConfig)
    {
        DataConfig = shopItemDataConfig;
        id = DataConfig.ID;
        Debug.Log($"Setting up item with ID: {id}");
        ImgIcon.sprite = DataConfig.spriteIcon;
    }

    private void OnClickSelectItem()
    {
        Debug.Log($"Selected item with ID: {id}");
        if (currentlySelectedItem != null && currentlySelectedItem != this)
        {
            currentlySelectedItem.OnInit();
        }
        currentlySelectedItem = this;
        UICShopSkin.Ins.ShowItemInfo(id);
        OnClick();
        EquipItemToPlayer();
    }

    public void UpdateItemState()
    {
        if (DataConfig != null)
        {
            if (DataConfig.isEquip)
            {
                GoLock.SetActive(false);
                equippedText.SetActive(true);
            }
            else if (DataConfig.isBought)
            {
                GoLock.SetActive(false);
                equippedText.SetActive(false);
            }
            else
            {
                GoLock.SetActive(true);
                equippedText.SetActive(false);
            }
        }
    }

    public void SetDefaultSkin()
    {
        switch (DataConfig.eskinType)
        {
            case EskinType.Hat:
                GameData.Ins.SetHatForPlayer(10, player.fullSetItem.hatPos);
                break;
            case EskinType.Pant:
                GameData.Ins.SetPantForPlayer(10, player.fullSetItem.PantRenderer);
                break;
            case EskinType.Shield:
                GameData.Ins.SetShieldForPlayer(0, player.fullSetItem.shieldPos);
                break;
            case EskinType.SkinSet:
                player.PlayerSetSkin(0);
                break;
        }
    }

    private void EquipItemToPlayer()
    {
        if (DataConfig != null)
        {
            intList.Add(id); // Track the currently equipped item ID

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
        }
    }

    public void EquipBuff()
    {
        switch (DataConfig.eBuffType)
        {
            case EBuffType.AttackRange:
                // Implement buff logic
                break;
            case EBuffType.MoveSpeed:
                // Implement buff logic
                break;
            case EBuffType.Gold:
                // Implement buff logic
                break;
            default: 
                break;
        }
    }
}
