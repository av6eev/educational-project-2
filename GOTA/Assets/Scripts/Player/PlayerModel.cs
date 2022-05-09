using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public event Action OnMove;

        public bool IsJump;
        public bool IsCrouch;
        public bool IsGrounded;
        public bool IsSprint;
        
        public Vector3 Position { get; private set; }
        public Vector3 Velocity;

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
            OnMove?.Invoke();
        }
    }
}