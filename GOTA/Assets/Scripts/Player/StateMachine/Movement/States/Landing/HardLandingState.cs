using System;
using Player.StateMachine.Movement.States.Landing.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Landing
{
    public class HardLandingState : LandingState
    {
        public HardLandingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            MoveAction.Disable();
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            
            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            
            MoveAction.Enable();
        }

        public override void OnAnimationExit()
        {
            MoveAction.Enable();
        }

        public override void OnAnimationTransition()
        {
            StateMachine.ChangeState(StateMachine.IdlingState);
        }

        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            
            MoveAction.started += OnMoveStarted;
        }
        
        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            
            MoveAction.started -= OnMoveStarted;
        }

        protected override void OnJump(InputAction.CallbackContext ctx)
        {
        }

        private void OnMoveStarted(InputAction.CallbackContext ctx)
        {
            OnMove();
        }
    }
}