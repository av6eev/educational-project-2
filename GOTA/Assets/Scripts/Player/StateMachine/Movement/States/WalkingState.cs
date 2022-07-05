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

            switch (Context.PlayerModel.IsButtonToggled)
            {
                case false:
                    MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
                    break;
                case true when Context.PlayerModel.IsRunEnable:
                    MovementStateMachine.ChangeState(MovementStateMachine.RunningState);
                    break;
            }
        }
    }
}