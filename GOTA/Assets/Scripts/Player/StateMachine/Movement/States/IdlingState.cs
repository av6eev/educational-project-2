using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class IdlingState : MovementState
    {
        public IdlingState(MovementStateMachine movementStateMachine, GameContext context) : base(movementStateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Context.PlayerData.SpeedModifier = 0f;
            ResetVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (MovementInput == Vector2.zero) return;

            OnMove();
        }

        private void OnMove()
        {
            if (Context.PlayerModel.IsButtonToggled)
            {
                MovementStateMachine.ChangeState(MovementStateMachine.WalkingState);
                Debug.Log($"{Context.PlayerModel.IsButtonToggled}; walking state");
                return;
            }
            
            // MovementStateMachine.ChangeState(); //skill cast system            
            
            Debug.Log($"{Context.PlayerModel.IsButtonToggled}; skill cast system");
        }
    }
}