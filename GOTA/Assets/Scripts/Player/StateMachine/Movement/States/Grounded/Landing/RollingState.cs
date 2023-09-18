using Player.Data.States;
using Player.StateMachine.Movement.States.Grounded.Landing.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Landing
{
    public class RollingState : LandingState
    {
        private readonly RollData _rollData;
        
        public RollingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _rollData = GroundedData.RollData;
        }

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = _rollData.SpeedModifier;
            
            base.Enter();
            
            SetupAnimation(View.AnimationsData.RollingHash, true);
            
            StateMachine.ReusableData.IsRunning = false;
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.RollingHash, false);
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

        protected override void OnJump(InputAction.CallbackContext ctx){}
    }
}