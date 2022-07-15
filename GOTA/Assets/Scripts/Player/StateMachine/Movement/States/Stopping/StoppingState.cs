using Player.StateMachine.Movement.States.Grounded;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Stopping
{
    public class StoppingState : GroundedState
    {
        public StoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
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