using Player.StateMachine.Movement.States.Moving.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Moving
{
    public class RunningState : MovingState
    {
        public RunningState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateMachine.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StrongForce;
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.HardStoppingState);
        }
    }
}