using Player.StateMachine.Movement.States.Grounded.Stopping.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Stopping
{
    public class HardStoppingState : StoppingState
    {
        public HardStoppingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}
        
        public override void Enter()
        {
            base.Enter();

            SetupAnimation(View.AnimationsData.HardStoppingHash, true);
            
            StateMachine.ReusableData.DecelerationForce = GroundedData.StopData.HardDecelerationForce;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StrongForce;
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.HardStoppingHash, false);
        }
    }
}