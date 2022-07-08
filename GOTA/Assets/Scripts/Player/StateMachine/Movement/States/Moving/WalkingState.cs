using Utilities;

namespace Player.StateMachine.Movement.States.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.MovementSpeedModifier = GroundedData.WalkData.SpeedModifier;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (StateMachine.ReusableData.IsButtonToggled)
            {
                case false:
                    StateMachine.ChangeState(StateMachine.IdlingState);
                    break;
                case true when Context.PlayerModel.IsRunEnable:
                    StateMachine.ChangeState(StateMachine.RunningState);
                    break;
            }
        }
    }
}