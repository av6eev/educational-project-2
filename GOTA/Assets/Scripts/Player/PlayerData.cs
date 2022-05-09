using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 51)]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        public float DefaultSpeed = 5.0f;
        public float CrouchSpeed = 2.0f;
        public float SprintSpeed = 7.0f;
        public float RotationSpeed = 5.0f;

        public float CrouchColliderHeight = 1.5f;
        
        public float JumpHeight = 1.0f;
        public float Gravity = -9.8f;
        public float GravityMultiplier = 2f;

        [Header("Animation Smoothing")] 
        public float VelocityDampTime = 0.1f;
        public float RotationDampTime = 0.1f;
        public float AirControl = 0.5f;
    }
}