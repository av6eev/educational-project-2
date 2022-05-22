using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public CharacterController CharacterController;
        public Transform CameraTransform;
        public Animator Animator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

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