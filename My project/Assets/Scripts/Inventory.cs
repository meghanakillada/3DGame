 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    HashSet<KeyColor> keys = new();

    public bool HasKey(KeyColor c) => keys.Contains(c);

    public void AddKey(KeyColor c)
    {
        if (keys.Add(c))
        {
            //UIManager.Instance.SetKeyIcon(c, true);
            //AudioManager.Instance.PlayKeyPickup();
        }
    }
}