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

    // [SerializeField] private Button lockButton;
    // [SerializeField] private Button equippedButton;
    // [SerializeField] private Button unEquippedButton;
    // [SerializeField] private Button XButton;
    // [SerializeField] private TextMeshProUGUI money;

    // private int selectedItemID;
    // private int previouslyEquippedItemID;
    // private UIShopItem selectedItemUI;
    // private UIShopItem equippedItemUI;

    private void Awake()
    {
        UICShopSkin.ins = this;
    }

    void Start()
    {
        // lockButton.onClick.AddListener(Check);
        // equippedButton.onClick.AddListener(EquipItem);
        // unEquippedButton.onClick.AddListener(UnequipSkin);
        // XButton.onClick.AddListener(OnXButtonClick);
        InitializeShopItems();

        // Load equipped item ID
        // previouslyEquippedItemID = PlayerPrefs.GetInt("PlayerSkin", 0);
        // EquipPreviouslyEquippedItem();
    }

    private void InitializeShopItems()
    {
        foreach (Transform child in pos)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < shopItemDataSO.dataConfigs.Count; i++)
        {
            UIShopItem uiItem = Instantiate(UIShopItemPrefab, pos);
            uiItem.Setup(shopItemDataSO.dataConfigs[i]);
        }
    }

    // private void UnequipSkin()
    // {
    //     if (equippedItemUI != null)
    //     {
    //         equippedItemUI.SetEquip(false);
    //         equippedItemUI = null;
    //     }
    //     LevelManager.Ins.player.PlayerSetSkin(0);
    //     equippedButton.gameObject.SetActive(true);
    //     unEquippedButton.gameObject.SetActive(false);
    // }

    public void ShowItemInfo(int id)
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
        if (itemConfig != null)
        {
            
        }
    }

    // public void ShowBoughtItemInfo(int id)
    // {
    //     ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
    //     if (itemConfig != null)
    //     {
    //         selectedItemID = id;
    //         selectedItemUI = FindUIShopItemById(id);

    //         lockButton.gameObject.SetActive(false);
    //         equippedButton.gameObject.SetActive(!selectedItemUI.isEquip);
    //         unEquippedButton.gameObject.SetActive(selectedItemUI.isEquip);

    //         money.text = "Bought";
    //     }
    // }

    // private void Check()
    // {
    //     lockButton.gameObject.SetActive(false);
    //     equippedButton.gameObject.SetActive(true);

    //     if (selectedItemUI != null)
    //     {
    //         selectedItemUI.SetBought();
    //         EquipItem();
    //     }
    // }

    // private void EquipItem()
    // {
    //     if (selectedItemUI != null)
    //     {
    //         if (equippedItemUI != null)
    //         {
    //             equippedItemUI.SetEquip(false);
    //         }

    //         equippedItemUI = selectedItemUI;
    //         selectedItemUI.SetEquip(true);

    //         previouslyEquippedItemID = selectedItemID; // Store the ID of the equipped item
    //         PlayerPrefs.SetInt("PlayerSkin", previouslyEquippedItemID); // Save the equipped item ID
    //         PlayerPrefs.Save(); // Ensure the data is saved

    //         equippedButton.gameObject.SetActive(false);
    //         unEquippedButton.gameObject.SetActive(true);
    //     }
    // }

    // public void OnXButtonClick()
    // {
    //     bool anyItemEquipped = false;
    //     foreach (Transform child in pos)
    //     {
    //         UIShopItem uiShopItem = child.GetComponent<UIShopItem>();
    //         if (uiShopItem != null && uiShopItem.isEquip)
    //         {
    //             anyItemEquipped = true;
    //             break;
    //         }
    //     }

    //     if (!anyItemEquipped)
    //     {
    //         if (selectedItemID != previouslyEquippedItemID)
    //         {
    //             LevelManager.Ins.player.PlayerSetSkin(previouslyEquippedItemID); // Re-equip the previously equipped item
    //         }
    //         else
    //         {
    //             LevelManager.Ins.player.PlayerSetSkin(selectedItemID); // Re-equip the selected item
    //         }
    //     }

    //     equippedButton.gameObject.SetActive(false);
    //     unEquippedButton.gameObject.SetActive(false);
    // }

    // private UIShopItem FindUIShopItemById(int id)
    // {
    //     foreach (Transform child in pos)
    //     {
    //         UIShopItem uiShopItem = child.GetComponent<UIShopItem>();
    //         if (uiShopItem != null && uiShopItem.id == id)
    //         {
    //             return uiShopItem;
    //         }
    //     }
    //     return null;
    // }

    // private void EquipPreviouslyEquippedItem()
    // {
    //     if (previouslyEquippedItemID > 0 && previouslyEquippedItemID < 10)
    //     {
    //         LevelManager.Ins.player.PlayerSetHat(previouslyEquippedItemID);
    //     }
    //     else if (previouslyEquippedItemID >= 10 && previouslyEquippedItemID < 20)
    //     {
    //         LevelManager.Ins.player.PlayerSetPant(previouslyEquippedItemID - 10);
    //     }
    //     else if (previouslyEquippedItemID >= 20 && previouslyEquippedItemID < 30)
    //     {
    //         LevelManager.Ins.player.PlayerSetShield(previouslyEquippedItemID - 20);
    //     }
    //     else if (previouslyEquippedItemID >= 30 && previouslyEquippedItemID < 40)
    //     {
    //         LevelManager.Ins.player.PlayerSetSkin(previouslyEquippedItemID - 30);
    //     }

    //     UIShopItem previouslyEquippedItemUI = FindUIShopItemById(previouslyEquippedItemID);
    //     if (previouslyEquippedItemUI != null)
    //     {
    //         previouslyEquippedItemUI.SetEquip(true);
    //         equippedItemUI = previouslyEquippedItemUI;
    //     }
    // }
}
