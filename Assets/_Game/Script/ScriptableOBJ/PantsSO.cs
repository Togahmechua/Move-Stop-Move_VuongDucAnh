using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantsSO", menuName = "ScriptableObjects/PantsSO", order = 1)]
public class PantsSO : ScriptableObject
{
    [SerializeField] private Material[] matsArr;

    public Material GetMaterial(EMats index)
    {
        return matsArr[(int)index];
    }
}

public enum EMats
{
    Batman = 0,
    Chambi = 1,
    Comy = 2,
    Dabao = 3,
    Onion = 4,
    Pokemon = 5,
    Rainbow = 6,
    Skull = 7,
    Vantim = 8
}
