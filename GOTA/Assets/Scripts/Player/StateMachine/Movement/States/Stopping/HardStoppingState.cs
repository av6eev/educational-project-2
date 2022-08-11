using Utilities;

namespace Player.StateMachine.Movement.States.Stopping
{
    public class HardStoppingState : StoppingState
    {
        public HardStoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.DecelerationForce = GroundedData.StopData.HardDecelerationForce;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StrongForce;
        }
    }
}