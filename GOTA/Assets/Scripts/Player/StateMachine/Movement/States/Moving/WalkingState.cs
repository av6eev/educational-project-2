using Player.StateMachine.Movement.States.Moving.Base;
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

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.LightStoppingState);
        }
    }
}