using System;
using UnityEngine;

namespace Player.Data.States
{
    [Serializable]
    public class AnimationsData
    {
        public int GroundedHash { get; private set; }
        public int MovingHash { get; private set; }
        public int StoppingHash { get; private set; }
        public int LandingHash { get; private set; }
        public int AirborneHash { get; private set; }

        public int IdleHash { get; private set; }
        public int WalkHash { get; private set; }
        public int DashHash { get; private set; }
        public int RunHash { get; private set; }
        public int HardStoppingHash { get; private set; }
        public int RollingHash { get; private set; }
        public int HardLandingHash { get; private set; }

        public int FallHash { get; private set; }

        [SerializeField] private string _grounded = "Grounded";
        [SerializeField] private string _moving = "Moving";
        [SerializeField] private string _stopping = "Stopping";
        [SerializeField] private string _landing = "Landing";
        [SerializeField] private string _airborne = "Airborne";

        [SerializeField] private string _isIdling = "IsIdling";
        [SerializeField] private string _isWalking = "IsWalking";
        [SerializeField] private string _isDashing = "IsDashing";
        [SerializeField] private string _isRunning = "IsRunning";
        [SerializeField] private string _isHardStopping = "IsHardStopping";
        [SerializeField] private string _isRolling = "IsRolling";
        [SerializeField] private string _isHardLanding = "IsHardLanding";

        [SerializeField] private string _isFalling = "IsFalling";

        public void Initialize()
        {
            GroundedHash = Animator.StringToHash(_grounded);
            MovingHash = Animator.StringToHash(_moving);
            StoppingHash = Animator.StringToHash(_stopping);
            LandingHash = Animator.StringToHash(_landing);
            AirborneHash = Animator.StringToHash(_airborne);
            IdleHash = Animator.StringToHash(_isIdling);
            WalkHash = Animator.StringToHash(_isWalking);
            DashHash = Animator.StringToHash(_isDashing);
            RunHash = Animator.StringToHash(_isRunning);
            HardStoppingHash = Animator.StringToHash(_isHardStopping);
            RollingHash = Animator.StringToHash(_isRolling);
            HardLandingHash = Animator.StringToHash(_isHardLanding);
            FallHash = Animator.StringToHash(_isFalling);
        }
    }
}