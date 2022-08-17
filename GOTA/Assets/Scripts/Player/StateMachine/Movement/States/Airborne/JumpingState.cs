using Player.Data.States;
using Player.StateMachine.Movement.States.Airborne.Base;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Airborne
{
    public class JumpingState : AirborneState
    {
        private JumpData _jumpData;
        private bool _isKeepRotating;
        private bool _canStartFalling;
        
        public JumpingState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _jumpData = AirborneData.JumpData;
            StateMachine.ReusableData.RotationData = _jumpData.RotationData;
        }

        public override void Enter()
        {
            base.Enter();
            
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            StateMachine.ReusableData.DecelerationForce = _jumpData.DecelerationForce;
            _isKeepRotating = StateMachine.ReusableData.MovementInput != Vector2.zero;

            OnJump();
        }

        public override void Exit()
        {
            base.Exit();
            
            SetRotationData();
            _canStartFalling = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_canStartFalling && IsMovingUp(0f))
            {
                _canStartFalling = true;
            }
            
            if (!_canStartFalling || GetVerticalVelocity().y > 0) return;
            
            StateMachine.ChangeState(StateMachine.FallingState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (_isKeepRotating)
            {
                RotateTowardsTarget();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        private void OnJump()
        {
            var jumpForce = StateMachine.ReusableData.CurrentJumpForce;
            var jumpDirection = View.transform.forward;

            if (_isKeepRotating)
            {   
                jumpDirection = GetTargetRotationDirection(StateMachine.ReusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            var center = View.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var ray = new Ray(center, Vector3.down);
            
            if (Physics.Raycast(ray, out var hit, _jumpData.RayDistance, View.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                var groundAngle = Vector3.Angle(hit.normal, -ray.direction);
                
                if (IsMovingUp())
                {
                    var forceModifier = _jumpData.UpwardsForceMultiplier.Evaluate(groundAngle);
                    
                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }
                
                if (IsMovingDown())
                {
                    var forceModifier = _jumpData.DownwardsForceMultiplier.Evaluate(groundAngle);
                    
                    jumpForce.y *= forceModifier;
                }
            }
            
            ResetVelocity();
            
            View.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
    }
} 