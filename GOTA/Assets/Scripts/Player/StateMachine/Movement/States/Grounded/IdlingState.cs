using Player.StateMachine.Movement.States.Grounded.Base;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded
{
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;

            base.Enter();
            
            SetupAnimation(View.AnimationsData.IdleHash, true);
            
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StationaryForce;
            
            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.IdleHash, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (StateMachine.ReusableData.MovementInput == Vector2.zero) return;
            
            OnMove();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally()) return;
            
            ResetVelocity();
        }
    }
}