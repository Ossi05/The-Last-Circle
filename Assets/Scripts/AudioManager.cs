using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClipsSO audioClips;

    public static AudioManager Instance;

    const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    float volume = 1f;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    void Start()
    {   
        BaseEnemy.OnAnyEnemyKilled += BaseEnemy_OnAnyEnemyDied;
    }

    void OnDestroy()
    {
        BaseEnemy.OnAnyEnemyKilled -= BaseEnemy_OnAnyEnemyDied;
    }

    void BaseEnemy_OnAnyEnemyDied(object sender, System.EventArgs e)
    {
        PlaySound(audioClips.explosions, audioClips.ExplosionVolume, Camera.main.transform.position);

    }

    public void PlayCountDownClip(int clipIndex)
    {
        PlaySound(audioClips.CountDownClips[clipIndex], audioClips.CountDownVolume, Camera.main.transform.position);
    }

    public void PlayShootingSound()
    {
        PlaySound(audioClips.shoot, audioClips.ShootVolume, Camera.main.transform.position);
    }

    void PlaySound(AudioClip[] sounds, float volume, Vector3 position)
    {
        AudioClip randomSound = sounds[Random.Range(0, sounds.Length)];
        PlaySound(randomSound, volume, position);
    }

    void PlaySound(AudioClip sound, float volumeMultiplier, Vector3 position)
    {
        if (position == null)
        {
            position = Camera.main.transform.position;
        }

        AudioSource.PlayClipAtPoint(sound, position, volume * volumeMultiplier);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1.1)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() 
    {
        return volume;
    }

}
