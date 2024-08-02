using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
    public TextMeshProUGUI rank;
    private int point;

    private void Start()
    {
        point = LevelManager.Ins.currentLevelInstance.remainingBotCount + 1;
    }

    public void UpdateText()
    {
        rank.text = "#" + point;
    }
}
