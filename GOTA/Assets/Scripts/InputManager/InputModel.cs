using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace InputManager
{
    public class InputModel
    {
        public readonly Dictionary<string, InputAction> InputActions = new Dictionary<string, InputAction>();
    }
}