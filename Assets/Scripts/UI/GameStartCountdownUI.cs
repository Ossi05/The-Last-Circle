using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    const string POPUP_TRIGGER = "Popup";
    Animator animator;

    int previousCountdownNumber;
    bool finishedCountdown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Gamemanager_OnStateChanged;
        Hide();
    }

    private void Gamemanager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStart())
        {            
            Show();
        }
        else
        {   
            
            if (!finishedCountdown)
            {
                finishedCountdown = true;
                countdownText.text = "GO!";
                AudioManager.Instance.PlayCountDownClip(0);
                animator.SetTrigger(POPUP_TRIGGER);
                StartCoroutine(HideAfterSeconds(1));
            }
            
            
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsCountDownToStart()) { return; }

        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countdownText.text = countDownNumber.ToString();

        if (countDownNumber != previousCountdownNumber)
        {   
            AudioManager.Instance.PlayCountDownClip(countDownNumber);
            animator.SetTrigger(POPUP_TRIGGER);
            previousCountdownNumber = countDownNumber;
        }
    }

    IEnumerator HideAfterSeconds(int second)
    {
        yield return new WaitForSeconds(second);
        Hide();
    }

    void Show()
    {
        finishedCountdown = false;
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
