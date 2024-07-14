using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICPantSkin : MonoBehaviour
{
    private static UICPantSkin ins;
    public static UICPantSkin Ins => ins;

    [SerializeField] private ShopItemData shopItemDataSO;
    [SerializeField] private UIShopPant UIShopPantPrefab;
    [SerializeField] private Transform pos;

    [SerializeField] private Button lockButton;
    [SerializeField] private Button equippedButton;
    [SerializeField] private Button unEquippedButton;
    [SerializeField] private Button XButton;
    [SerializeField] private TextMeshProUGUI money;

    private int selectedItemID;
    private UIShopPant selectedItemUI;
    private UIShopPant equippedItemUI;

    private void Awake()
    {
        UICPantSkin.ins = this;
    }

    void Start()
    {
        lockButton.onClick.AddListener(Check);
        equippedButton.onClick.AddListener(EquipItem);
        unEquippedButton.onClick.AddListener(UnequipSkin);
        XButton.onClick.AddListener(OnXButtonClick);
        InitializeShopItems();
    }

    private void InitializeShopItems()
    {
        foreach (Transform child in pos)
        {
            Destroy(child.gameObject); // Clear previous items
        }

        for (int i = 0; i < shopItemDataSO.dataConfigs.Count; i++)
        {
            UIShopPant uiItem = Instantiate(UIShopPantPrefab, pos);
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
        GameData.Ins.SetSkinForPlayer(0 , LevelManager.Ins.player.body, LevelManager.Ins.player.pants, LevelManager.Ins.player.hatPos, LevelManager.Ins.player.wingPos, LevelManager.Ins.player.tailPos, LevelManager.Ins.player.shieldPos);
        equippedButton.gameObject.SetActive(true);
        unEquippedButton.gameObject.SetActive(false);
    }

    public void ShowItemInfo(int id)
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
        if (itemConfig != null)
        {
            selectedItemID = id;
            selectedItemUI = FindUIShopPantById(id);

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
            selectedItemUI = FindUIShopPantById(id);

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

    public void OnXButtonClick()
    {
        bool anyItemEquipped = false;
        foreach (Transform child in pos)
        {
            UIShopPant uisUIShopPant = child.GetComponent<UIShopPant>();
            if (uisUIShopPant != null && uisUIShopPant.isEquip)
            {
                anyItemEquipped = true;
                break;
            }
        }

        if (!anyItemEquipped)
        {
            GameData.Ins.SetSkinForPlayer(0 , LevelManager.Ins.player.body, LevelManager.Ins.player.pants, LevelManager.Ins.player.hatPos, LevelManager.Ins.player.wingPos, LevelManager.Ins.player.tailPos, LevelManager.Ins.player.shieldPos);
        }

        equippedButton.gameObject.SetActive(false);
        unEquippedButton.gameObject.SetActive(false);
    }

    private UIShopPant FindUIShopPantById(int id)
    {
        foreach (Transform child in pos)
        {
            UIShopPant uisUIShopPant = child.GetComponent<UIShopPant>();
            if (uisUIShopPant != null && uisUIShopPant.id == id)
            {
                return uisUIShopPant;
            }
        }
        return null;
    }
}
