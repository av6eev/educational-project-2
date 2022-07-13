using InputManager;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public class GlobalContainer : MonoBehaviour
    {
        public InputComponent InputComponent;
        
        public PlayerInput PlayerInput;
        public PlayerView PlayerView;
    }
}