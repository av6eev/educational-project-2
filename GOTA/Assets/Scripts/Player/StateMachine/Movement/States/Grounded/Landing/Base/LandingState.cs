using Player.StateMachine.Movement.States.Grounded.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Landing.Base
{
    public class LandingState : GroundedState
    {
        protected LandingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            base.Enter();
            
            SetupAnimation(View.AnimationsData.LandingHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.LandingHash, false);
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx){}
    }
}