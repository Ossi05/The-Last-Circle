using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputBinding;
using UnityEngine.UIElements;
using System;

public class InstructionText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructionText;
    [SerializeField] InputActionReference actionType;
    [SerializeField] string instruction;

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (Controls.Instance == null) { return; }

        string controlScheme = Controls.Instance.GetCurrentControlScheme();
        InputAction action = actionType.action;

        if (action == null) { return; }

        string key = "";

        foreach (var binding in action.bindings)
        {
            if (binding.groups.Contains(controlScheme))
            {
                key += binding.ToDisplayString();
            }
        }


        instructionText.text = instruction.Replace("{key}", key);

    }
}
