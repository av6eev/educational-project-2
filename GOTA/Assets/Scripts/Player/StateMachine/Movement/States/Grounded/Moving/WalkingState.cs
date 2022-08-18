using Player.StateMachine.Movement.States.Grounded.Moving.Base;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Moving
{
    public class WalkingState : MovingState
    {
        public WalkingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = GroundedData.WalkData.SpeedModifier;

            base.Enter();

            SetupAnimation(View.AnimationsData.WalkHash, true);
            
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.WalkHash, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!StateMachine.ReusableData.IsButtonToggled)
            {
                StateMachine.ChangeState(StateMachine.IdlingState);
                return;
            }
            
            if (StateMachine.ReusableData.IsRunning)
            {
                StateMachine.ChangeState(StateMachine.RunningState);
            }
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.LightStoppingState);
        }
    }
}