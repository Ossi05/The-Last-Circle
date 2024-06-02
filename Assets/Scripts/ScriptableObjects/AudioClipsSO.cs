using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioClipsSO", menuName = "AudioClips/New AudioClipSO")]
public class AudioClipsSO : ScriptableObject
{
    public AudioClip shoot;
    public float ShootVolume = 1f;
    public AudioClip[] explosions;
    public float ExplosionVolume = 1f;
    public AudioClip[] CountDownClips;
    public float CountDownVolume = 1f;
}
