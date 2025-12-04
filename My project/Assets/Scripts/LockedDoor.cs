using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public KeyColor color;
    public GameObject blocker;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Inventory>(out var inv))
        {
            if (inv.HasKey(color))
            {
                Unlock();
            }
        }
    }

    public void Unlock()
    {
        if (blocker) blocker.SetActive(false);
        Destroy(this); 
    }
}
