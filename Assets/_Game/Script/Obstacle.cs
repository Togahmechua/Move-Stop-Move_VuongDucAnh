using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Material[] materialsArr;
    [SerializeField] private Renderer ren;

    public void Blur()
    {
        //Lam mo
        ren.material = materialsArr[0];
    }

    public void DefaultColor()
    {
         //Lam mo
        ren.material = materialsArr[1];
    }
}
