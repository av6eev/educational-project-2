using Player.StateMachine.Movement.States.Base;
using Player.Utilities.Data;
using UnityEngine;
using Utilities;

namespace Player.StateMachine.Movement.States.Grounded.Base
{
    public class GroundedState : MovementState
    {
        private SlopeData _slopeData;

        protected GroundedState(MovementStateMachine stateMachine, GameContext context) : base(stateMachine, context)
        {
            _slopeData = View.ColliderUtility.SlopeData;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            OnMove();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Floating();
        }

        private void Floating()
        {
            var worldColliderCenter = View.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var ray = new Ray(worldColliderCenter, Vector3.down);

            if (Physics.Raycast(ray, out var hit, _slopeData.RayDistance, View.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                var groundAngle = Vector3.Angle(hit.normal, -ray.direction);
                var slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

                if (slopeSpeedModifier == 0f) return;

                var distanceToFloatingPoint = View.ColliderUtility.CapsuleColliderData.LocalColliderCenter.y * View.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f) return;

                var amountToLift = distanceToFloatingPoint * _slopeData.StepReachForce - GetVerticalVelocity().y;
                var liftForce = new Vector3(0f, amountToLift, 0f);

                View.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            var slopeSpeedModifier = GroundedData.SlopeSpeedAngles.Evaluate(angle);

            StateMachine.ReusableData.OnSlopeSpeedModifier = slopeSpeedModifier;
            return slopeSpeedModifier;
        }

        protected override void OnGroundExited()
        {
            base.OnGroundExited();
            
            var worldColliderCenter = View.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var ray = new Ray(worldColliderCenter - View.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);
            
            if (!Physics.Raycast(ray, out _, GroundedData.GroundToFallRayDistance, View.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                OnFall();                
            }
        }
        
        protected virtual void OnFall()
        {
            StateMachine.ChangeState(StateMachine.FallingState);
        }
        
        protected virtual void OnMove()
        {
            switch (StateMachine.ReusableData.IsButtonToggled)
            {
                case false:
                    StateMachine.ChangeState(StateMachine.IdlingState);
                    //TODO: skill cast system
                    break;
                case true when StateMachine.ReusableData.MovementInput != Vector2.zero:
                    if (StateMachine.ReusableData.IsRunning)
                    {
                        StateMachine.ChangeState(StateMachine.RunningState);
                    }
                    else
                    {
                        StateMachine.ChangeState(StateMachine.WalkingState);
                    }
                    break;
                default:
                    if (StateMachine.ReusableData.IsGrounded)
                    {
                        StateMachine.ChangeState(StateMachine.IdlingState);
                    }
                    break;
            }
        }
    }
}