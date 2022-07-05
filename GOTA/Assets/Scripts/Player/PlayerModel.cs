using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public event Action<bool> OnWalk;
        public event Action<float> OnIdle;
        public event Action OnToggle;
        public event Action OnRun;

        public bool IsRunEnable;
        
        public Vector3 Position { get; private set; }

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
        }

        public void ButtonToggled()
        {
            OnToggle?.Invoke();
        }

        public void RunEnabled()
        {
            IsRunEnable = !IsRunEnable;
            OnRun?.Invoke();
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