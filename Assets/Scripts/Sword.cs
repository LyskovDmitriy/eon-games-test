using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private new Collider collider = default;


    public void SetColliderEnabled(bool isEnabled)
    {
        collider.enabled = isEnabled;
    }
}
