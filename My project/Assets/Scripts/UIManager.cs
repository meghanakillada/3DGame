using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    public TMP_Text livesText;
    public GameObject redKeyIcon, blueKeyIcon, greenKeyIcon, yellowKeyIcon;

    [Header("Menus")]
    public GameObject pausePanel;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void UpdateLives(int lives) => livesText.text = $"Lives: {lives}";
    public void SetKeyIcon(KeyColor kc, bool on)
    {
        switch (kc)
        {
            case KeyColor.Red: redKeyIcon.SetActive(on); break;
            case KeyColor.Blue: blueKeyIcon.SetActive(on); break;
            case KeyColor.Green: greenKeyIcon.SetActive(on); break;
            case KeyColor.Yellow: yellowKeyIcon.SetActive(on); break;
        }
    }

    public void ShowPause(bool on) => pausePanel.SetActive(on);

    public void LoadWinScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Win");
    }

    public void LoadLoseScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lose");
    }

    // Buttons
    public void Resume() => GameManager.Instance.TogglePause();
    public void Quit() => GameManager.SafeQuit();
}