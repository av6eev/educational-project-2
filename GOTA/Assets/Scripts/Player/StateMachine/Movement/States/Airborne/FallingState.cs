using Player.Data.States;
using Player.StateMachine.Movement.States.Airborne.Base;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne
{
    public class FallingState : AirborneState
    {
        private readonly FallData _fallData;
        private Vector3 _positionOnEnter;
        
        public FallingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _fallData = AirborneData.FallData;
        }

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;

            base.Enter();
            
            SetupAnimation(View.AnimationsData.FallHash, true);
            
            _positionOnEnter = View.transform.position;
            
            ResetVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.FallHash, false);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        protected override void OnGroundContact()
        {
            var fallDistance = _positionOnEnter.y - View.transform.position.y;

            if (fallDistance < _fallData.MinDistanceToHardFall)
            {
                StateMachine.ChangeState(StateMachine.LightLandingState);
                return;
            }

            if (StateMachine.ReusableData.MovementInput == Vector2.zero)
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