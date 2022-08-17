using InputManager;
using Player.Data.States;
using Player.StateMachine.Movement.States.Grounded;
using Player.StateMachine.Movement.States.Grounded.Base;
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
        private bool _isKeepRotating;
        
        public DashingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _dashData = GroundedData.DashData;
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;
            StateMachine.ReusableData.CurrentJumpForce = AirborneData.JumpData.StrongForce;
            StateMachine.ReusableData.RotationData = _dashData.RotationData;
            
            Dash();
            _isKeepRotating = StateMachine.ReusableData.MovementInput != Vector2.zero;
            UpdateConsecutiveDashes();
            
            _startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();
            SetRotationData();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (!_isKeepRotating) return;
            RotateTowardsTarget();
        }

        public override void OnAnimationTransition()
        {
            base.OnAnimationTransition();
            
            if (StateMachine.ReusableData.MovementInput == Vector2.zero)
            {  
                StateMachine.ChangeState(StateMachine.HardStoppingState);
                return;
            }

            OnMove();
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

        private void Dash()
        {
            if (StateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            var rotationDirection = View.transform.forward;
            rotationDirection.y = 0f;
            
            UpdateTargetRotation(rotationDirection, false);
            View.Rigidbody.velocity = rotationDirection * GetMovementSpeed();
        }
        
        protected override void AddInputCallbacks()
        {
            base.AddInputCallbacks();
            MoveAction.performed += OnMovePerformed;
        }

        protected override void RemoveInputCallbacks()
        {
            base.RemoveInputCallbacks();
            MoveAction.performed -= OnMovePerformed;
        }
        
        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            _isKeepRotating = true;
        }
        
        protected override void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
        }

        protected override void OnDash(InputAction.CallbackContext ctx)
        {
        }
    }
}