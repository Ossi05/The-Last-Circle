using System;
using UnityEngine;

public class FadingInstruction : MonoBehaviour
{
    [SerializeField] Animator fadeOutAnimatior;

    const string FADEOUT = "Fade";

    void Start()
    {
        if (fadeOutAnimatior != null)
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }
    }

    void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStart())
        {
            fadeOutAnimatior.SetTrigger(FADEOUT);
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }
    }

    void Hide()
    {   
        // Called from animation event
        gameObject.SetActive(false);
    }
}
