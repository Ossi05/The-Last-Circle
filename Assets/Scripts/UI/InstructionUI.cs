using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour
{   
    public static InstructionUI Instance;

    [SerializeField] Button continueButton;

    void Awake()
    {
        Instance = this;

        continueButton.onClick.AddListener(() =>
        {   
            GameManager.Instance.StartCountDown();
            Hide();
        });

        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
}
