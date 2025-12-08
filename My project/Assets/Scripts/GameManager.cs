using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Refs")]
    public FirstPersonController player;
    public Transform startSpawn;
    public Transform finalExit; // optional marker

    [Header("Rules")]
    public int maxLives = 3;

    int lives;
    bool paused;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        lives = maxLives;
        //UIManager.Instance.UpdateLives(lives);
        Debug.Log($"GameManager Awake: maxLives = {maxLives}, lives = {lives}");
        Time.timeScale = 1f;
    }

    public void PlayerCaught()
    {
        Debug.Log("GameManager: PlayerCaught called");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayJumpscare();
        }

        lives--;

        if (lives <= 0)
        {
            Debug.Log("GameManager: Out of lives → full restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (player != null && startSpawn != null)
        {
            Debug.Log("GameManager: Respawning player at start (keeping keys)");
            player.ResetTo(startSpawn);
        }
    }

    public void PlayerEscaped()
    {
        //UIManager.Instance.ShowEnd("You Survived! (Barely)");
    }

    public void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        //UIManager.Instance.ShowPause(paused);
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public static void SafeQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public bool IsPaused => Time.timeScale == 0f;
}