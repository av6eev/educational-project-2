using Player.StateMachine.Movement.States.Grounded.Landing.Base;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Landing
{
    public class LightLandingState : LandingState
    {
        public LightLandingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StationaryForce;
            ResetVelocity();
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

        public override void OnAnimationTransition()
        {
            StateMachine.ChangeState(StateMachine.IdlingState);
        }
    }
}