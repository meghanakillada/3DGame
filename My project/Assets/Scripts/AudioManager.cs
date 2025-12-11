using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [Tooltip("Main AudioSource for one-shot SFX (footsteps, pickups, jumpscares, etc.)")]
    public AudioSource sfxOneShot;

    [Header("Audio Clips")]
    public AudioClip footstepPlayer;
    public AudioClip footstepEnemy;
    public AudioClip monsterBreathLoop;
    public AudioClip keyPickup;
    public AudioClip jumpscare;
    public AudioClip ambientLoop;

    [Header("Volume Settings - SFX")]
    [Range(0f, 1f)] public float sfxMasterVolume = 1.0f;
    [Range(0f, 1f)] public float footstepPlayerVolume = 0.5f;
    [Range(0f, 1f)] public float footstepEnemyVolume = 0.6f;
    [Range(0f, 1f)] public float keyPickupVolume = 0.7f;
    [Range(0f, 1f)] public float jumpscareVolume = 1.0f;

    [Header("Volume Settings - Loops")]
    [Range(0f, 1f)] public float ambientVolume = 0.4f;
    [Range(0f, 1f)] public float monsterBreathBaseVolume = 0.3f;

    private AudioSource ambientSource;
    private AudioSource monsterSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ----------------------
        // Ambient Background Loop
        // ----------------------
        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.clip = ambientLoop;
        ambientSource.loop = true;
        ambientSource.spatialBlend = 0f; // 2D
        ambientSource.volume = ambientVolume;

        if (ambientLoop != null)
        {
            ambientSource.Play();
        }

        // ----------------------
        // Monster Breathing Loop
        // ----------------------
        if (monsterBreathLoop != null)
        {
            monsterSource = gameObject.AddComponent<AudioSource>();
            monsterSource.clip = monsterBreathLoop;
            monsterSource.loop = true;
            monsterSource.spatialBlend = 0f; // global; we scale volume manually
            monsterSource.volume = monsterBreathBaseVolume;
            monsterSource.Play();
        }
    }

    // ----------------------
    // Public SFX Functions
    // ----------------------

    public void PlayFootstepPlayer()
    {
        if (footstepPlayer != null && sfxOneShot != null)
        {
            sfxOneShot.PlayOneShot(footstepPlayer, footstepPlayerVolume * sfxMasterVolume);
        }
    }

    public void PlayFootstepEnemy()
    {
        if (footstepEnemy != null && sfxOneShot != null)
        {
            sfxOneShot.PlayOneShot(footstepEnemy, footstepEnemyVolume * sfxMasterVolume);
        }
    }

    public void PlayKeyPickup()
    {
        if (keyPickup != null && sfxOneShot != null)
        {
            sfxOneShot.PlayOneShot(keyPickup, keyPickupVolume * sfxMasterVolume);
        }
    }

    public void PlayJumpscare()
    {
        if (jumpscare != null && sfxOneShot != null)
        {
            sfxOneShot.PlayOneShot(jumpscare, jumpscareVolume * sfxMasterVolume);
        }
    }

    // Volume here is proximity (0–1), we multiply by base volume
    public void SetMonsterBreathingVolume(float proximityVolume)
    {
        if (monsterSource != null)
        {
            proximityVolume = Mathf.Clamp01(proximityVolume);
            monsterSource.volume = proximityVolume * monsterBreathBaseVolume;
        }
    }

    // Optional: call this if you change ambientVolume at runtime and want to apply it.
    public void RefreshLoopVolumes()
    {
        if (ambientSource != null)
            ambientSource.volume = ambientVolume;

        if (monsterSource != null)
            monsterSource.volume = monsterBreathBaseVolume;
    }
}