using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne
{
    public class AirborneState : MovementState
    {
        private readonly bool _isGrounded;
        public AirborneState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
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