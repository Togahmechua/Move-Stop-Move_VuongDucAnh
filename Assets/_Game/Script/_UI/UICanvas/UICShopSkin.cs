using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
    private static UICShopSkin ins;
    public static UICShopSkin Ins => ins;

    [SerializeField] protected Player player;
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
    
    private List<ShopItemDataConfig> shopItemDatasList = new List<ShopItemDataConfig>();
    private List<int> intList = new List<int>(); // List to keep track of equipped item IDs

    private void Awake()
    {
        UICShopSkin.ins = this;
    }

    void Start()
    {
        lockButton.onClick.AddListener(CheckLock);
        equippedButton.onClick.AddListener(EquipItem);
        unEquippedButton.onClick.AddListener(UnequipSkin);
        XButton.onClick.AddListener(ExitButton);
        InitializeShopItems();
    }

    private void InitializeShopItems()
    {
        foreach (Transform child in pos)
        {
            Destroy(child.gameObject);
        }

        shopItemDatasList.Clear(); // Clear the list before adding new items

        for (int i = 0; i < shopItemDataSO.dataConfigs.Count; i++)
        {
            UIShopItem uiItem = Instantiate(UIShopItemPrefab, pos);
            ShopItemDataConfig dataConfig = shopItemDataSO.dataConfigs[i];
            uiItem.Setup(dataConfig);

            shopItemDatasList.Add(dataConfig);
        }
    }

    public void ShowItemInfo(int id)
    {
        Debug.Log($"Showing item info for ID: {id}");
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == id);
        if (itemConfig != null)
        {
            selectedItemID = id;
            selectedItemUI = FindUIShopItemById(id);

            LoadButton(itemConfig);
        }
    }

    public void LoadButton(ShopItemDataConfig itemConfig)
    {
        //lock
        if (itemConfig.isLock)
        {
            lockButton.gameObject.SetActive(true);
            equippedButton.gameObject.SetActive(false);
            unEquippedButton.gameObject.SetActive(false);
            money.text = itemConfig.Price.ToString();
        }
        //unlock
        else if (!itemConfig.isLock && !itemConfig.isEquip && !itemConfig.isUnequip && itemConfig.isBought)
        {
            lockButton.gameObject.SetActive(false);
            equippedButton.gameObject.SetActive(true);
            unEquippedButton.gameObject.SetActive(false);
            selectedItemUI.GoLock.SetActive(false);
        }
        //equip
        else if (itemConfig.isEquip)
        {
            lockButton.gameObject.SetActive(false);
            equippedButton.gameObject.SetActive(false);
            unEquippedButton.gameObject.SetActive(true);
            selectedItemUI.equippedText.SetActive(true);

            // Set all other items to unequip
            foreach (var s in shopItemDatasList)
            {
                if (s.ID != itemConfig.ID && s.isEquip)
                {
                    s.isEquip = false;
                    s.isUnequip = true;
                    s.isBought = true;
                    UIShopItem uiShopItem = FindUIShopItemById(s.ID);
                    if (uiShopItem != null)
                    {
                        uiShopItem.SetDefaultSkin();
                    }
                }
            }

            // Update the list and UI
            UpdateAllItemsState();
        }
        //unequip
        else if (itemConfig.isUnequip && !itemConfig.isEquip && itemConfig.isBought)
        {
            lockButton.gameObject.SetActive(false);
            equippedButton.gameObject.SetActive(true);
            unEquippedButton.gameObject.SetActive(false);
            selectedItemUI.equippedText.SetActive(false);
        }
    }

    private void CheckLock()
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == selectedItemID);
        if (itemConfig != null)
        {
            CoinManager coinManager = LevelManager.Ins.coinMNG;
            if (coinManager.coin < itemConfig.Price)
            {
                coinManager.NotEnoughMoney(itemConfig.Price);
            }
            else
            {
                itemConfig.isLock = false;
                itemConfig.isEquip = false;
                itemConfig.isBought = true;
                coinManager.DecreaseMoney(itemConfig.Price);
                LoadButton(itemConfig);
            }
        }
    }

    private void EquipItem()
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == selectedItemID);
        if (itemConfig != null)
        {
            itemConfig.isEquip = true;
            itemConfig.isUnequip = false; // Ensure that isUnequip is set to false when equipping
            intList.Add(selectedItemID); // Add the item ID to the list when equipped
            LoadButton(itemConfig);
        }
    }

    private void UnequipSkin()
    {
        ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == selectedItemID);
        if (itemConfig != null)
        {
            itemConfig.isUnequip = true;
            itemConfig.isEquip = false;
            LoadButton(itemConfig);
        }
    }

    private void UpdateAllItemsState()
    {
        foreach (var itemConfig in shopItemDatasList)
        {
            UIShopItem uiShopItem = FindUIShopItemById(itemConfig.ID);
            if (uiShopItem != null)
            {
                uiShopItem.UpdateItemState();
            }
        }
    }

    public void ExitButton()
    {
        Debug.Log("ExitButton called");
        bool itemFound = false; // Flag to determine if an item is found

        foreach (int itemId in intList)
        {
            ShopItemDataConfig itemConfig = shopItemDataSO.dataConfigs.Find(item => item.ID == itemId);

            if (itemConfig != null && itemConfig.isBought && itemConfig.isEquip)
            {
                Debug.Log("A"); // Log "A" if item with isBought and isEquip is found
                itemFound = true;
                break;
            }
        }

        if (!itemFound)
        {
            // No item with isBought and isEquip found, set player to default skin
            Debug.Log("No equipped and bought item found; setting player to default skin.");
            player.PlayerSetSkin(0);
        }

        // Clear the list and update all items
        intList.Clear();
        UpdateAllItemsState();
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
