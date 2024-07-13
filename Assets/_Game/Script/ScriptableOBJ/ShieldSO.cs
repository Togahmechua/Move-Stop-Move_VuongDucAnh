using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldSO", menuName = "ScriptableObjects/ShieldSO", order = 1)]
public class ShieldSO : ScriptableObject
{
    [SerializeField] private GameObject[] ShieldPrefab; 

    public GameObject GetShield(EShield index)
    {
        return ShieldPrefab[(int) index];
    }
}

public enum EShield
{
    None = 0,
    BlackShield = 1,
    CAPShield = 2
}
