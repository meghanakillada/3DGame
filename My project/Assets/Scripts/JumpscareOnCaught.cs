using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareOnCaught : MonoBehaviour
{
    public bool destroyOnCatch = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // 1. Play jumpscare sound
        AudioManager.Instance.PlayJumpscare();

        // 2. Notify GameManager that the player was caught
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerCaught();
        }
        else
        {
            Debug.LogWarning("JumpscareOnCaught: GameManager missing!");
        }

        // Optional: remove this trigger after use
        if (destroyOnCatch)
            Destroy(gameObject);
    }
}