using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class RunningState : MovementState
    {
        public RunningState(MovementStateMachine movementStateMachine , GameContext context) : base(movementStateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Context.PlayerData.SpeedModifier = 1f;
        }
    }
}