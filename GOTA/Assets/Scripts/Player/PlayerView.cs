using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public CharacterController CharacterController;
        public Transform CameraTransform;
        public Animator Animator;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsJump = Animator.StringToHash("IsJump");
        private static readonly int IsLand = Animator.StringToHash("IsLand");
        private static readonly int IsSprint = Animator.StringToHash("IsSprint");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public void Jump(bool isEnable)
        {
            Animator.SetFloat(Speed, 0);
            Animator.SetBool(IsJump, isEnable);
        }
        
        public void Land(bool isEnable)
        {
            Animator.SetFloat(Speed, 0);
            Animator.SetBool(IsLand, isEnable);
        }

        public void Sprint(Vector2 input, PlayerData data, bool isEnable)
        {
            Animator.SetFloat(Speed, input.magnitude + 0.5f, data.SpeedDampTime, Time.deltaTime);
            Animator.SetBool(IsSprint, isEnable);
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