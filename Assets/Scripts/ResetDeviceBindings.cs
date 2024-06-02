using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ResetDeviceBindings : MonoBehaviour {
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] ControlScheme targetControlScheme;
    [SerializeField] Button resetBindingsButton;

    void Awake()
    {
        resetBindingsButton.onClick.AddListener(() =>
        {
            ResetControlSchemeBindings();
        });
    }

    enum ControlScheme {
        Keyboard,
        Gamepad,
    }

    void ResetControlSchemeBindings()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
        {
            foreach (InputAction action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    if (action.bindings[i].groups.Contains(targetControlScheme.ToString()))
                    {
                        // Remove the binding override for the action
                        action.RemoveBindingOverride(i);
                    }
                }
            }
        }

    }

}
