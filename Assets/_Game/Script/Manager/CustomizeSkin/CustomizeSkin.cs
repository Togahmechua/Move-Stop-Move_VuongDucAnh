using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeSkin : MonoBehaviour
{
    [SerializeField] private BodyPartData[] bodyPartDatas;

    public enum BodyPartType
    {
        Hat,
        Pants,
        Weapon
    }

    [Serializable]
    public class BodyPartData
    {
        public BodyPartType bodyPartType;
        public Transform pos;
    }
}

