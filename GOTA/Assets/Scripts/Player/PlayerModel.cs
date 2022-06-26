using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public event Action<bool> OnWalk;
        public event Action<float> OnIdle;
        public event Action OnToggle;

        public bool IsButtonToggled;
        
        public bool IsWalk;
        public bool IsGrounded;
        public bool IsRotationButtonEnable;
        
        public Vector3 Position { get; private set; }

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
        }

        public void ButtonToggled()
        {
            IsButtonToggled = !IsButtonToggled;
            OnToggle?.Invoke();
        }
        
        public void Walk(bool isEnable)
        {
            OnWalk?.Invoke(isEnable);
        }

        public void Idle(float speed)
        {
            OnIdle?.Invoke(speed);
        }
    }
}