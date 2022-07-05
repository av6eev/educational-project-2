using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class IdlingState : MovementState
    {
        public IdlingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            ResetVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            OnMove();
        }
        
        private void OnMove()
        {
            if (StateMachine.ReusableData.IsButtonToggled)
            {
                if (StateMachine.ReusableData.MovementInput != Vector2.zero)
                {
                    StateMachine.ChangeState(StateMachine.WalkingState);
                    Debug.Log($"Toggle: {StateMachine.ReusableData.IsButtonToggled}; walking state");
                    return;
                }
            }
            else
            {
                // StateMachine.ChangeState(); //skill cast system            
                Debug.Log($"Toggle: {StateMachine.ReusableData.IsButtonToggled}; skill cast system");
            }
        }
    }
}