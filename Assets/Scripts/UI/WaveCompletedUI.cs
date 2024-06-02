using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCompletedUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;

    const string POPUP_TRIGGER = "Popup";
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        WaveManager.Instance.OnWaveCompleted += WaveManager_OnWaveCompleted;
        Hide();
    }

    private void WaveManager_OnWaveCompleted(object sender, System.EventArgs e)
    {
        Show();
        waveText.text = $"WAVE {WaveManager.Instance.GetCurrentWaveIndex() + 1}/{WaveManager.Instance.GetTotalWaveCount()} COMPLETED";
        animator.SetTrigger(POPUP_TRIGGER);

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
