using Player.Data.States;
using Player.StateMachine.Movement.States.Airborne.Base;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne
{
    public class FallingState : AirborneState
    {
        private FallData _fallData;
        private Vector3 _positionOnEnter;
        
        public FallingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _fallData = AirborneData.FallData;
        }

        public override void Enter()
        {
            base.Enter();
            
            _positionOnEnter = View.transform.position;
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            
            ResetVerticalVelocity();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        protected override void OnGroundContact()
        {
            var fallDistance = Mathf.Abs(_positionOnEnter.y - View.transform.position.y);

            if (fallDistance < _fallData.MinDistanceToHardFall)
            {
                StateMachine.ChangeState(StateMachine.LightLandingState);
                return;
            }

            if (!StateMachine.ReusableData.IsRunning && StateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                StateMachine.ChangeState(StateMachine.HardLandingState);
                return;
            }
            
            StateMachine.ChangeState(StateMachine.RollingState);
        }
 
        private void LimitVerticalVelocity()
        {
            var verticalVelocity = GetVerticalVelocity();
            
            if (verticalVelocity.y >= -_fallData.SpeedLimit) return;

            var limitedVelocity = new Vector3(0f, -_fallData.SpeedLimit - View.Rigidbody.velocity.y, 0f);
            
            View.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
        }
    }
}