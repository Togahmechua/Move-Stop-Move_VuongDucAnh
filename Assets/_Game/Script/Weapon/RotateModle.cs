using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private Transform model;

    void Update()
    {
        if (model != null)
        {
            model.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
