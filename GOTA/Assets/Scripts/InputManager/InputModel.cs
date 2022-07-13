using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace InputManager
{
    public class InputModel
    {
        public event Action<InputAction, float> OnDisableAction;

        public readonly Dictionary<string, InputAction> InputActions = new Dictionary<string, InputAction>();

        public void DisableAction(InputAction action, float seconds)
        {
            OnDisableAction?.Invoke(action, seconds);
        }
    }
}