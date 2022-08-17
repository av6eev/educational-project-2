using Player.StateMachine.Movement.States.Stopping.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Stopping
{
    public class MediumStoppingState : StoppingState 
    {
        public MediumStoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.DecelerationForce = GroundedData.StopData.MediumDecelerationForce;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.MediumForce;
        }
    }
}