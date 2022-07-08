using Player.StateMachine.Movement.States.Grounded;
using Utilities;

namespace Player.StateMachine.Movement.States.Moving
{
    public class MovingState : GroundedState
    {
        public MovingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }
    }
}