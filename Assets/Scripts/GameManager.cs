using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 
    public static GameManager Instance;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    [SerializeField] float countDownToStartTimer = 3f;
    [SerializeField] float gameEndDelayOnVictory = 5f;

    state currentState;
    
    bool gameWon = false;
    float gamePlayingTimer;
    int enemiesKilled;
    bool gamePaused;
    bool startCountdown;

    enum state {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver,
    }

    void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
    }

    void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        MainBase.Instance.OnBaseDestroyed += MainBase_OnBaseDestroyed;
        WaveManager.Instance.OnAllWavesCompleted += EnemyWaveSpawner_OnAllWavesCompleted;
        BaseEnemy.OnAnyEnemyKilled += BaseEnemy_OnAnyEnemyKilled;
        Controls.Instance.OnGamePaused += Controls_OnGamePaused;
    }
    void Controls_OnGamePaused()
    {
        TogglePauseGame();
    }

    private void BaseEnemy_OnAnyEnemyKilled(object sender, EventArgs e)
    {
         enemiesKilled++;
    }

    private void EnemyWaveSpawner_OnAllWavesCompleted(object sender, EventArgs e)
    {
        gameWon = true;
        StartCoroutine(GameEndDelay());
    }

    private void MainBase_OnBaseDestroyed(object sender, System.EventArgs e)
    {
        HandleGameLose();
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        HandleGameLose();
    }

    void HandleGameLose()
    {   
        gameWon = false;
        currentState = state.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator GameEndDelay()
    {
        yield return new WaitForSeconds(gameEndDelayOnVictory);
        currentState = state.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }


    void Update()
    {
        switch (currentState)
        {
            case state.WaitingToStart:
                if (startCountdown)
                {
                    MusicPlayer.Instance.StopMusic();
                    currentState = state.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case state.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    currentState = state.Playing;
                    MusicPlayer.Instance.PlayGameMusic();
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case state.Playing:
                gamePlayingTimer += Time.deltaTime;
                break;
            case state.GameOver:
                break;
        }
    }

    public bool IsGameOver()
    {
        return currentState == state.GameOver;
    }

    public bool IsGameWon()
    {
        return gameWon;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public float GetGamePlayTime()
    {
        return gamePlayingTimer;
    }

    public bool IsGamePlaying()
    {
        return currentState == state.Playing;
    }

    public bool IsCountDownToStart()
    {
        return currentState == state.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    private void OnDestroy()
    {
        BaseEnemy.OnAnyEnemyKilled -= BaseEnemy_OnAnyEnemyKilled;
    }

    public void StartCountDown()
    {
        startCountdown = true;
    }

    public void TogglePauseGame()
    {   
        if (currentState == state.WaitingToStart)
        {
            InstructionUI.Instance.Hide();
            StartCountDown();
            return;
        }

        if (gamePaused)
        {
            if (SettingsUI.Instance.IsActive())
            {
                SettingsUI.Instance.Hide();
                return;
            }
        }

        gamePaused = !gamePaused;

        if (gamePaused)
        {   
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {   
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
