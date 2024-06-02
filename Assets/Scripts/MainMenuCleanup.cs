using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCleanup : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        BaseEnemy.ResetStaticData();
    }

    void Start()
    {
        MusicPlayer.Instance.PlayMainMenuMusic();
    }
}
