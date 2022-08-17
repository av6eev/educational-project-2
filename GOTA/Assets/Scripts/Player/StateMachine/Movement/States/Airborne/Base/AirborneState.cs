using Player.StateMachine.Movement.States.Base;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne.Base
{
    public class AirborneState : MovementState
    {
        private readonly bool _isGrounded;
        protected AirborneState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
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