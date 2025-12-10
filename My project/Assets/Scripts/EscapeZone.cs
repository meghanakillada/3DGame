using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EscapeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // check if the thing that entered is the player
        if (other.GetComponentInParent<FirstPersonController>() != null)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerEscaped();
            }
            else
            {
                Debug.LogWarning("EscapeZone: GameManager.Instance is NULL");
            }
        }
    }
}

