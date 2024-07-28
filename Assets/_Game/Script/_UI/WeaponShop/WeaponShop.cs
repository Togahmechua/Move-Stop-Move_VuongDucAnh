using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    public GameObject[] weaponModels;
    public WeaponConfig[] weaponConfigs;
    public Button buyButton;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button preButton;
    [SerializeField] private bool enableToNext = true;
    [SerializeField] private bool enableToBack = true;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        nextButton.onClick.AddListener(NextButton);
        preButton.onClick.AddListener(PreviousButton);
        foreach (WeaponConfig wp in weaponConfigs)
        {
            if (wp.price == 0)
            {
                wp.isUnlocked = true;
            }
            else
            {
                wp.isUnlocked = PlayerPrefs.GetInt(wp.name, 0) == 1;
            }
        }

        foreach (GameObject weapon in weaponModels)
        {
            weapon.SetActive(false);
        }

        weaponModels[currentWeaponIndex].SetActive(true);
        UpdateUI();
    }

    public void Update()
    {
        if (currentWeaponIndex >= weaponModels.Length - 1)
        {
            enableToNext = false;
            nextButton.interactable = false;
        }
        else if (currentWeaponIndex <= 0)
        {
            enableToBack = false;
            preButton.interactable = false;
        }
        else
        {
            enableToNext = true;
            enableToBack = true;
            nextButton.interactable = true;
            preButton.interactable = true;
        }
    }

    public void NextButton()
    {
        if (!enableToNext) return;
        weaponModels[currentWeaponIndex].SetActive(false);

        currentWeaponIndex++;
        weaponModels[currentWeaponIndex].SetActive(true);
        UpdateUI();
    }

    public void PreviousButton()
    {
        if (!enableToBack) return;
        weaponModels[currentWeaponIndex].SetActive(false);

        currentWeaponIndex--;
        weaponModels[currentWeaponIndex].SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        WeaponConfig wp = weaponConfigs[currentWeaponIndex];
        buyButton.onClick.RemoveAllListeners();
        if (wp.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            wp.equipButton.gameObject.SetActive(true);
            buyButton.onClick.AddListener(() => EquipWeapon(currentWeaponIndex));
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            wp.equipButton.gameObject.SetActive(false);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = wp.price.ToString();
            if (wp.price <= LevelManager.Ins.coinMNG.coin)
            {
                buyButton.onClick.AddListener(BuyClick);
            }
            else
            {
                buyButton.onClick.AddListener(NotEnoughMoneyClick);
            }
        }
    }

    private void NotEnoughMoneyClick()
    {
        WeaponConfig wp = weaponConfigs[currentWeaponIndex];
        LevelManager.Ins.coinMNG.NotEnoughMoney(wp.price);
    }

    private void BuyClick()
    {
        WeaponConfig wp = weaponConfigs[currentWeaponIndex];
        LevelManager.Ins.coinMNG.DecreaseMoney(wp.price);
        wp.isUnlocked = true;
        PlayerPrefs.SetInt(wp.name, 1);
        UpdateUI();
    }

    private void EquipWeapon(int weaponIndex)
    {
        player.ChangeWeapon(weaponIndex);
        PlayerPrefs.SetInt("PWeapon", weaponIndex);
    }
}
