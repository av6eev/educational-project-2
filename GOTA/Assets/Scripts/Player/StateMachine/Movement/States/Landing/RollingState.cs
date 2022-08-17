using Player.Data.States;
using Player.StateMachine.Movement.States.Landing.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Landing
{
    public class RollingState : LandingState
    {
        private RollData _rollData;
        
        public RollingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _rollData = GroundedData.RollData;
        }

        public override void Enter()
        {
            base.Enter();
            
            StateMachine.ReusableData.MovementSpeedModifier = _rollData.SpeedModifier;
            StateMachine.ReusableData.IsRunning = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (StateMachine.ReusableData.MovementInput != Vector2.zero) return;
            
            RotateTowardsTarget();
        }

        public override void OnAnimationTransition()
        {
            base.OnAnimationTransition();
            
            if (StateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                StateMachine.ChangeState(StateMachine.MediumStoppingState);
                return;
            }
            
            OnMove();
        }

        protected override void OnJump(InputAction.CallbackContext ctx)
        {
        }
    }
}