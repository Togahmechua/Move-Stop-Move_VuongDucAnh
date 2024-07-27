using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coin;

    private void Start()
    {
        // coin = PlayerPrefs.GetInt("Money", 0);
        UpdateText();
    }

    public void IncreaseMoney(int amount)
    {
        coin += amount;
        // PlayerPrefs.SetInt("Money", coin);
        UpdateText();
    }

    public void DecreaseMoney(int amount)
    {
        coin -= amount;
        // PlayerPrefs.SetInt("Money", coin);
        UpdateText();   
    }

    public void UpdateText()
    {
        if (coin <= 999)
        {
            coinText.text = coin.ToString();
        }
        else
        {
            int thousands = coin / 1000;
            int hundreds = (coin % 1000) / 100;
            if (hundreds == 0)
            {
                coinText.text = $"{thousands}k";
            }
            else
            {
                coinText.text = $"{thousands}k{hundreds}";
            }
        }
    }

    public void NotEnoughMoney(int amount)
    {
        if (coin < amount)
        {
            StartCoroutine(CoinEffect());
        }
    }

    private IEnumerator CoinEffect()
    {
        coinText.color = Color.red;
        yield return new WaitForSeconds(1f);
        coinText.color = Color.white;
    }
}
