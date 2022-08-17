using Player.StateMachine.Movement.States.Grounded.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Moving.Base
{
    public class MovingState : GroundedState
    {
        protected MovingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }
    }
}