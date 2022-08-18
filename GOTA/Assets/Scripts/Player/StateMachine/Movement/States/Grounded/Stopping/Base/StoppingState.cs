using Player.StateMachine.Movement.States.Grounded.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Stopping.Base
{
    public class StoppingState : GroundedState
    {
        protected StoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            
            base.Enter();

            SetupAnimation(View.AnimationsData.StoppingHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.StoppingHash, false);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            RotateTowardsTarget();
            
            if (!IsMovingHorizontally()) return;
            
            DecelerateHorizontally();
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
    }
}