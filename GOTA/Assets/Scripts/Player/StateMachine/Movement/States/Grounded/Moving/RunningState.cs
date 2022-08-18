using Player.StateMachine.Movement.States.Grounded.Moving.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Moving
{
    public class RunningState : MovingState
    {
        public RunningState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context){}

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = GroundedData.RunData.SpeedModifier;
            
            base.Enter();

            SetupAnimation(View.AnimationsData.RunHash, true);
            
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StrongForce;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!StateMachine.ReusableData.IsButtonToggled)
            {
                StateMachine.ChangeState(StateMachine.IdlingState);
                return;
            }
            
            if (StateMachine.ReusableData.MovementInput != Vector2.zero && !StateMachine.ReusableData.IsRunning)
            {
                StateMachine.ChangeState(StateMachine.HardStoppingState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            SetupAnimation(View.AnimationsData.RunHash, false);
        }

        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.HardStoppingState);
        }
    }
}