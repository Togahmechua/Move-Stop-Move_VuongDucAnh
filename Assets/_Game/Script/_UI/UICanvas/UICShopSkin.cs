using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
    private static UICShopSkin ins;
    public static UICShopSkin Ins => ins;

    [SerializeField] private ShopItemData shopItemDataSO;
    [SerializeField] private UIShopItem UIShopItemPrefab;
    [SerializeField] private Transform pos;

    [SerializeField] private Button lockButton;
    [SerializeField] private Button equippedButton;
    [SerializeField] private Button unEquippedButton;
    [SerializeField] private Button XButton;
    [SerializeField] private TextMeshProUGUI money;

    private int selectedItemID;
    private UIShopItem selectedItemUI;
    private UIShopItem equippedItemUI;

    private void Awake()
    {
        UICShopSkin.ins = this;
    }

    void Start()
    {
        lockButton.onClick.AddListener(Check);
        equippedButton.onClick.AddListener(EquipItem);
        unEquippedButton.onClick.AddListener(UnequipSkin);
        XButton.onClick.AddListener(OnXButtonClick);
        for (int i = 0; i < shopItemDataSO.dataConfigs.Count; i++)
        {
            UIShopItem uiItem = Instantiate(UIShopItemPrefab, pos);
            uiItem.Setup(shopItemDataSO.dataConfigs[i]);
        }
    }

    private void UnequipSkin()
    {
        if (equippedItemUI != null)
        {
            equippedItemUI.SetEquip(false);
            equippedItemUI = null;
        }
        GameData.Ins.SetHatForPlayer(10, LevelManager.Ins.player.hatPos);
        equippedButton.gameObject.SetActive(true);
        unEquippedButton.gameObject.SetActive(false);
    }

    public void ShowItemInfo(int id)
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
        if (itemConfig != null)
        {
            selectedItemID = id;
            selectedItemUI = FindUIShopItemById(id);

            lockButton.gameObject.SetActive(!selectedItemUI.IsBought);
            equippedButton.gameObject.SetActive(selectedItemUI.IsBought && !selectedItemUI.isEquip);
            unEquippedButton.gameObject.SetActive(selectedItemUI.IsBought && selectedItemUI.isEquip);

            money.text = itemConfig.Price.ToString("");
        }
    }

    public void ShowBoughtItemInfo(int id)
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
        if (itemConfig != null)
        {
            selectedItemID = id;
            selectedItemUI = FindUIShopItemById(id);

            lockButton.gameObject.SetActive(false);
            equippedButton.gameObject.SetActive(!selectedItemUI.isEquip);
            unEquippedButton.gameObject.SetActive(selectedItemUI.isEquip);

            money.text = "Bought";
        }
    }

    private void Check()
    {
        // Logic to check if the item can be bought
        lockButton.gameObject.SetActive(false);
        equippedButton.gameObject.SetActive(true);

        if (selectedItemUI != null)
        {
            selectedItemUI.SetBought();
        }
    }

    private void EquipItem()
    {
        if (selectedItemUI != null)
        {
            if (equippedItemUI != null)
            {
                equippedItemUI.SetEquip(false);
            }

            equippedItemUI = selectedItemUI;
            selectedItemUI.SetEquip(true);

            equippedButton.gameObject.SetActive(false);
            unEquippedButton.gameObject.SetActive(true);
        }
    }

    private void OnXButtonClick()
    {
        bool anyItemEquipped = false;
        foreach (Transform child in pos)
        {
            UIShopItem uiShopItem = child.GetComponent<UIShopItem>();
            if (uiShopItem != null && uiShopItem.isEquip)
            {
                anyItemEquipped = true;
                break;
            }
        }

        if (!anyItemEquipped)
        {
            GameData.Ins.SetHatForPlayer(10, LevelManager.Ins.player.hatPos);
        }

        equippedButton.gameObject.SetActive(false);
        unEquippedButton.gameObject.SetActive(false);
    }

    private UIShopItem FindUIShopItemById(int id)
    {
        foreach (Transform child in pos)
        {
            UIShopItem uiShopItem = child.GetComponent<UIShopItem>();
            if (uiShopItem != null && uiShopItem.id == id)
            {
                return uiShopItem;
            }
        }
        return null;
    }
}
