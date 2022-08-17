using Player.StateMachine.Movement.States.Stopping.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Stopping
{
    public class LightStoppingState : StoppingState
    {
        public LightStoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.DecelerationForce = GroundedData.StopData.LightDecelerationForce;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.WeakForce;
        }
    }
}