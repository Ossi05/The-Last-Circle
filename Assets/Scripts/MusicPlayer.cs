using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip mainMenuMusic;
    [Space]
    [SerializeField] AudioClip gameMusicStartClip;
    [SerializeField] AudioClip gameMusicLoopClip;

    public static MusicPlayer Instance;

    const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    AudioSource musicPlayer;
    float volume = 1f;

    void Awake()
    {   
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        musicPlayer = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        musicPlayer.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1.1)
        {
            volume = 0;
        }

        musicPlayer.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
    }

    public float GetVolume()
    {
        return volume;
    }
    
    public void PlayMainMenuMusic()
    {
        if (musicPlayer != null)
        {
            musicPlayer.clip = mainMenuMusic;
            musicPlayer.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (musicPlayer != null)
        {
            musicPlayer.clip = gameMusicStartClip;
            musicPlayer.Play();
            StartCoroutine(PlayGameMusicLoopClip());
        }
    }

    IEnumerator PlayGameMusicLoopClip()
    {
        float delay = 0.04f;
        yield return new WaitForSecondsRealtime(gameMusicStartClip.length - delay);
        musicPlayer.clip = gameMusicLoopClip;
        musicPlayer.Play();
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

}
