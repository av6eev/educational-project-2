using Player.StateMachine.Movement.States.Grounded.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Landing.Base
{
    public class LandingState : GroundedState
    {
        protected LandingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
        }
    }
}