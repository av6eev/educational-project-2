using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class RunningState : MovementState
    {
        public RunningState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (StateMachine.ReusableData.IsButtonToggled)
            {
                case false when !Context.PlayerModel.IsRunEnable:
                    StateMachine.ChangeState(StateMachine.IdlingState);
                    break;
                case true when !Context.PlayerModel.IsRunEnable:
                    StateMachine.ChangeState(StateMachine.WalkingState);
                    break;
                case false:
                    StateMachine.ChangeState(StateMachine.IdlingState);
                    break;
                default: return;
            }
        }
    }
}