using System;
using Player.Utilities;
using Player.Utilities.Data;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Transform CameraTransform;
        public Animator Animator;
        public ColliderUtility ColliderUtility;
        public LayerData LayerData;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        private void Awake()
        {
            CameraTransform = Camera.main.transform;
        }

        private void OnValidate()
        {
            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateColliderDimensions();
        }

        public void SetAnimationSpeed(float inputMagnitude, float speedDampTime, float deltaTime)
        {
            Animator.SetFloat(Speed, inputMagnitude, speedDampTime, deltaTime);
        }

        public void Walk(bool isEnable)
        {
            Animator.SetBool(IsWalk, isEnable);
        }

        public void Idle(float speed)
        {
            Animator.SetFloat(Speed, speed);
        }
    }
}