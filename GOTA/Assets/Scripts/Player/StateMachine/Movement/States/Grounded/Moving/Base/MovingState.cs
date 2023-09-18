using Player.StateMachine.Movement.States.Grounded.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Moving.Base
{
    public class MovingState : GroundedState
    {
        protected MovingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            base.Enter();

            SetupAnimation(View.AnimationsData.MovingHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.MovingHash, false);
        }
    }
}