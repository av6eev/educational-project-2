using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne
{
    public class JumpingState : AirborneState
    {
        public JumpingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.MovementSpeedModifier = 0f;

            OnJump();
        }

        private void OnJump()
        {
            var jumpForce = StateMachine.ReusableData.CurrentJumpForce;
            var forward = View.transform.forward;

            jumpForce.x *= forward.x;
            jumpForce.z *= forward.z;
            
            ResetVelocity();
            
            View.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
    }
} 