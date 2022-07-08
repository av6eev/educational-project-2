using UnityEngine;

namespace Player.Data.States
{
    public class StateReusableData
    {
        public Vector2 MovementInput { get; set; }
        public float MovementSpeedModifier { get; set; } = 1f;
        public float OnSlopeSpeedModifier { get; set; } = 1f;
        public bool IsButtonToggled { get; set; }

        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedCurrentVelocity;
        private Vector3 _dampedPassedTime;

        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
        public ref Vector3 DampedCurrentVelocity => ref _dampedCurrentVelocity;
        public ref Vector3 DampedPassedTime => ref _dampedPassedTime;
    }
}