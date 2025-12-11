using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBreathingController : MonoBehaviour
{
    public Transform player;
    public float maxHearDistance = 20f;

    void Update()
    {
        if (player == null || AudioManager.Instance == null)
            return;

        float dist = Vector3.Distance(transform.position, player.position);
        float volume = Mathf.Clamp01(1f - (dist / maxHearDistance));

        AudioManager.Instance.SetMonsterBreathingVolume(volume);
    }
}