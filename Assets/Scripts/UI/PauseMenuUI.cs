using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemiesKilledText;
    [SerializeField] TextMeshProUGUI totalPlayTimeText;
    [SerializeField] Button continueButton;
    [SerializeField] Button settingsButton;

    void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        settingsButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.Show();
        });
    }

    void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        enemiesKilledText.text = GameManager.Instance.GetEnemiesKilled().ToString();
        UpdateTimeText();
        Show();
    }

    void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }


    void UpdateTimeText()
    {
        float time = GameManager.Instance.GetGamePlayTime();
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        totalPlayTimeText.text = $"{minutes:00}:{seconds:00}";

    }
}
