using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverTitleText;
    [SerializeField] TextMeshProUGUI enemiesKilledText;
    [SerializeField] TextMeshProUGUI totalPlayTimeText;

    const string GAME_LOST_TEXT = "YOU LOSE!";
    const string GAME_WON_TEXT = "YOU WIN!";

    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        HandleGameOver();
    }

    async void HandleGameOver()
    {
        if (GameManager.Instance.IsGameOver())
        {   
            if (FadeTransition.Instance != null)
            {
                await FadeTransition.Instance.FadeInToOut();
            }

            if (GameManager.Instance.IsGameWon())
            {
                gameOverTitleText.text = GAME_WON_TEXT;
            }
            else
            {
                gameOverTitleText.text = GAME_LOST_TEXT;
            }

            enemiesKilledText.text = GameManager.Instance.GetEnemiesKilled().ToString();
            UpdateTimeText();

            Show();
        }
    }

    void UpdateTimeText()
    {
        float time = GameManager.Instance.GetGamePlayTime();
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        totalPlayTimeText.text = $"{minutes:00}:{seconds:00}";

    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

}
