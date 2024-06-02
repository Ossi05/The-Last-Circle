using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] Button settingsButton;

    void Awake()
    {
        settingsButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.Show();
        });
    }
}
