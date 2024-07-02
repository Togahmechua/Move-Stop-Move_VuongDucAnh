using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorsSO", menuName = "ScriptableObjects/ColorsSO", order = 1)]
public class ColorsSO : ScriptableObject
{
    [SerializeField] private Material[] matsArr;

    public Material GetColors(Ecolor index)
    {
        return matsArr[(int)index];
    }
}

public enum Ecolor
{
    Red = 0,
    Blue = 1,
    Green = 2,
    Orange = 3,
    Yellow = 4,
    Pink = 5,
    Purple = 6,
    Black = 7,
    Brown = 8
}
