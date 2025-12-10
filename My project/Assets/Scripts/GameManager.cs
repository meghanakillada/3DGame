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
    public Transform finalExit;

    [Header("Rules")]
    public int maxLives = 3;

    int lives;
    bool paused;
    bool gameOver;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        lives = maxLives;
        gameOver = false;
        Debug.Log($"GameManager Awake: maxLives = {maxLives}, lives = {lives}");
        Time.timeScale = 1f;
    }

    void Start()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
        }
    }

    public void PlayerCaught()
    {
        if (gameOver)
        {
            return; // ignore extra hits after game over
        }
        lives = Mathf.Max(lives - 1, 0); // decrement lives but not below 0
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
        }
        if (lives <= 0)
        {
            gameOver = true;
            Debug.Log("GameManager: Out of lives → Lose Scene");
            if (UIManager.Instance != null)
            {
                UIManager.Instance.LoadLoseScene();
            }
            else
            {
                SceneManager.LoadScene("Lose"); // fallback if UIManager is missing
            }
            return;
        }
        // if still have lives left, respawn at start
        if (player != null && startSpawn != null)
        {
            Debug.Log("GameManager: Respawning player at start (keeping keys)");
            // CAUSING ERROR FOR ME
            // player.ResetTo(startSpawn);

            player.transform.position = startSpawn.position;
            player.transform.rotation = startSpawn.rotation;

            // If the player has velocity (Starter Assets), clear it:
            var controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Teleport without friction issues
                controller.enabled = false;
                controller.transform.position = startSpawn.position;
                controller.transform.rotation = startSpawn.rotation;
                controller.enabled = true;
            }

        }
        else
        {
            Debug.LogWarning("GameManager: player or startSpawn is NULL in PlayerCaught");
        }
    }

    public void PlayerEscaped()
    {
        if (gameOver) return;

        Debug.Log("GameManager: Player Escaped → Win Scene");
        gameOver = true;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.LoadWinScene();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // press escape to pause/unpause
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (gameOver) return;
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        if (UIManager.Instance != null) UIManager.Instance.ShowPause(paused);
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
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