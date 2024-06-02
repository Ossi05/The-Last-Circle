using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance;

    [SerializeField] Button musicButton;
    [SerializeField] Button soundEffectsButton;
    [Space]
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] TextMeshProUGUI soundEffectsText;
    void Awake()
    {
        Instance = this;
        musicButton.onClick.AddListener(() =>
        {
            MusicPlayer.Instance.ChangeVolume();
            UpdateVisual();
        });
        soundEffectsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeVolume();
            UpdateVisual();
        });
    }

    void Start()
    {
        UpdateVisual();
        Hide();
    }

    void UpdateVisual()
    {
        soundEffectsText.text = $"Sound Effects {Mathf.Round(AudioManager.Instance.GetVolume() * 10f)}";
        musicText.text = $"Music {Mathf.Round(MusicPlayer.Instance.GetVolume() * 10f)}";
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
