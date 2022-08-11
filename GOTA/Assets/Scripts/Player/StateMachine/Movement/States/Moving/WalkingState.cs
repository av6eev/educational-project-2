using UnityEngine.InputSystem;
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
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.WeakForce;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            OnMove();
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.LightStoppingState);
        }
    }
}