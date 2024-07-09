using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    public Button BtnSelect;
    public Image ImgIcon;
    public GameObject GoLock;

    void Start()
    {
        BtnSelect.onClick.AddListener(OnClickSelectItem);
    }

    public void Setup() 
    {
        
    }

    private void OnClickSelectItem()
    {
        UIManager.Ins.GetUI<UICShopSkin>().ShowItemInfo(1);
    }
}
