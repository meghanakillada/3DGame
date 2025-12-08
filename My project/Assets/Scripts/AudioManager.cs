using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource sfxOneShot;
    public AudioClip footstepPlayer, footstepEnemy;
    public AudioClip monsterBreathLoop;
    public AudioClip keyPickup, jumpscare, ambientLoop;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Start ambient
        var amb = gameObject.AddComponent<AudioSource>();
        amb.clip = ambientLoop;
        amb.loop = true;
        amb.spatialBlend = 0f;
        amb.Play();
    }

    public void PlayKeyPickup() => sfxOneShot.PlayOneShot(keyPickup);
    public void PlayJumpscare() => sfxOneShot.PlayOneShot(jumpscare);
}
