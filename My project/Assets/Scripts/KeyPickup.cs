using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyColor color;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Inventory>(out var inv))
        {
            inv.AddKey(color);
            Destroy(gameObject);
        }
    }
}