using Player;
using Player.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities
{
    public class GlobalContainer : MonoBehaviour
    {
        public PlayerInput PlayerInput;
        public PlayerView PlayerView;
        public CameraView CameraView;
    }
}