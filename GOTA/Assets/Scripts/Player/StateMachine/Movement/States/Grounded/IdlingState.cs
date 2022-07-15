using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded
{
    public class IdlingState : GroundedState
    {
        public IdlingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
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
    }
}