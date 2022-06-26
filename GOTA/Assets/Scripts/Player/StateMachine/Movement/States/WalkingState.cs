using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class WalkingState : MovementState
    {
        public WalkingState(MovementStateMachine movementStateMachine, GameContext context) : base(movementStateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Context.PlayerData.SpeedModifier = 0.5f;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!Context.PlayerModel.IsButtonToggled)
            {
                MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
            }
        }
    }
}