using Player.StateMachine.Movement.States.Grounded.Landing.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Landing
{
    public class HardLandingState : LandingState
    {
        public HardLandingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;

            base.Enter();
            
            SetupAnimation(View.AnimationsData.HardLandingHash, true);
            
            MoveAction.Disable();
            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.HardLandingHash, false);
            
            MoveAction.Enable();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally()) return;
            ResetVelocity();
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

        private void OnMoveStarted(InputAction.CallbackContext ctx)
        {
            OnMove();
        }
        
        protected override void OnJump(InputAction.CallbackContext ctx){}
    }
}