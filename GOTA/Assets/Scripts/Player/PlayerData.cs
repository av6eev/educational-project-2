using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 51)]
    public class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        public float DefaultSpeed = 5.0f;
        public float RotationSpeed = 5.0f;

        public float Gravity = -9.8f;

        [Header("Animation Smoothing")] 
        public float SpeedDampTime = 0.1f;
        public float VelocityDampTime = 0.1f;
        public float RotationDampTime = 0.1f;
    }
}