using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatsSO", menuName = "ScriptableObjects/HatsSO", order = 1)]
public class HatsSO : ScriptableObject
{
    [SerializeField] private GameObject[] Hatprefab; 

    public GameObject GetHats(EHats index)
    {
        return Hatprefab[(int) index];
    }
}

public enum EHats
{
    Arrow = 0,
    Cowboy = 1,
    Crown = 2,
    Ear = 3,
    Hat = 4,
    Hat_Cap = 5,
    Hat_Yellow = 6,
    Headphone = 7,
    Horn = 8,
    Mustache = 9,
    None = 10
}
