using InputManager;
using Player.Data.States;
using Player.StateMachine.Movement.States.Grounded;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States.Moving
{
    public class DashingState : GroundedState
    {
        private DashData _dashData;
        private float _startTime;
        private int _consecutiveDashesUsed;
        
        public DashingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _dashData = GroundedData.DashData;
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;
            
            AddForce();
            UpdateConsecutiveDashes();
            
            _startTime = Time.time;
        }

        protected override void OnDash(InputAction.CallbackContext ctx)
        {
        }

        public override void OnAnimationTransition()
        {
            base.OnAnimationTransition();
            
            if (StateMachine.ReusableData.MovementInput == Vector2.zero)
            {  
                StateMachine.ChangeState(StateMachine.IdlingState);
                return;
            }

            StateMachine.ChangeState(StateMachine.RunningState);
        }

        private void UpdateConsecutiveDashes()
        {
            if (!IsConsecutive())
            {
                _consecutiveDashesUsed = 0;
            }

            ++_consecutiveDashesUsed;
            
            if (_consecutiveDashesUsed == _dashData.DashesLimitAmount)
            {
                _consecutiveDashesUsed = 0;
                
                Context.InputModel.DisableAction(Context.InputModel.InputActions[InputActionsConstants.Dash], _dashData.DashLimitCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return Time.time < _startTime + _dashData.ConsideredConsecutiveTime;
        }

        private void AddForce()
        {
            if (StateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            var rotationDirection = View.transform.forward;
            rotationDirection.y = 0f;
            View.Rigidbody.velocity = rotationDirection * GetMovementSpeed();
        }
    }
}