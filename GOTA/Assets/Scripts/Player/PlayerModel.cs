using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public event Action<bool> OnJump;
        public event Action<Vector2, PlayerData, bool> OnSprint;
        public event Action<bool> OnLand;
        public event Action<bool> OnWalk;
        public event Action<float> OnIdle;

        public bool IsJump;
        public bool IsWalk;
        public bool IsGrounded;
        public bool IsSprint;
        
        public Vector3 Position { get; private set; }

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
        }

        public void Walk(bool isEnable)
        {
            OnWalk?.Invoke(isEnable);
        }

        public void Jump(bool isEnable)
        {
            OnJump?.Invoke(isEnable);
        }
        
        public void Land(bool isEnable)
        {
            OnLand?.Invoke(isEnable);
        }

        public void Sprint(Vector2 input, PlayerData data, bool isEnable)
        {
            OnSprint?.Invoke(input, data, isEnable);
        }

        public void Idle(float speed)
        {
            OnIdle?.Invoke(speed);
        }
    }
}