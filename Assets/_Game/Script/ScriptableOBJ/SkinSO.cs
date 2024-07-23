using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSO", menuName = "ScriptableObjects/SkinSO", order = 1)]
public class SkinSO : ScriptableObject
{
    public List<FullSetItem> sets;
}


public enum ESetSkin
{
    None = 0,
    Devil = 1,
    Angle = 2,
    Witch = 3,
    DeadPool = 4,
    Thor = 5
}
