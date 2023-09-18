using Player.StateMachine.Movement.States.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne.Base
{
    public class AirborneState : MovementState
    {
        protected AirborneState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            base.Enter();
            
            SetupAnimation(View.AnimationsData.AirborneHash, true);
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.AirborneHash, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (StateMachine.ReusableData.IsGrounded)
            {
                StateMachine.ChangeState(StateMachine.IdlingState);
            }
        }
    }
}