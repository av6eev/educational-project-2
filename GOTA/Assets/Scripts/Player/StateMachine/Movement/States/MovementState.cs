﻿using InputManager;
using Player.Data.States;
using Player.StateMachine.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using IState = Player.StateMachine.Utilities.IState;

namespace Player.StateMachine.Movement.States
{
    public class MovementState : IState
    {
        protected readonly Rigidbody Rigidbody;
        protected readonly GameContext Context;
        protected readonly PlayerView View;

        protected InputAction ToggleButton;
        protected InputAction RunButton;
        
        protected readonly MovementStateMachine StateMachine;

        protected readonly GroundedData GroundedData;

        public MovementState(MovementStateMachine stateMachine, GameContext context)
        {
            StateMachine = stateMachine;
            Context = context;
            
            GroundedData = Context.PlayerSO.GroundedData;
            Rigidbody = Context.GlobalContainer.PlayerView.Rigidbody;
            View = Context.GlobalContainer.PlayerView;

            Initialize();
        }

        private void Initialize()
        {
            StateMachine.ReusableData.TimeToReachTargetRotation = GroundedData.RotationData.TargetRotationReachTime;
        }

        public virtual void Enter()
        {
            Debug.Log($"{GetType().Name} entered");

            AddInputCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputCallbacks();
        }

        public virtual void HandleInput()
        {
            StateMachine.ReusableData.MovementInput = Context.InputModel.InputActions[InputActionsConstants.Movement].ReadValue<Vector2>();
        }

        private void AddInputCallbacks()
        {
            ToggleButton = Context.InputModel.InputActions[InputActionsConstants.ToggleButton];
            RunButton = Context.InputModel.InputActions[InputActionsConstants.RunButton];

            ToggleButton.started += OnToggle;
            RunButton.started += OnRun;
            RunButton.canceled += OnRun;
        }

        private void RemoveInputCallbacks()
        {
            ToggleButton.started -= OnToggle;
            RunButton.started -= OnRun;
            RunButton.canceled -= OnRun;
        }

        private void OnRun(InputAction.CallbackContext ctx)
        {
            Context.PlayerModel.RunEnabled();
        }

        private void OnToggle(InputAction.CallbackContext ctx)
        {
            StateMachine.ReusableData.IsButtonToggled = !StateMachine.ReusableData.IsButtonToggled;
            Context.PlayerModel.ButtonToggled();
        }

        public virtual void LogicUpdate()
        {
            if (StateMachine.ReusableData.MovementInput == Vector2.zero || StateMachine.ReusableData.MovementSpeedModifier == 0f)
            {
                if (!Context.StateMachineEngine.GetStateMachine(StateMachineType.Movement).GetCurrentState().Equals(StateMachine.IdlingState))
                {
                    StateMachine.ChangeState(StateMachine.IdlingState);
                }
            }
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (StateMachine.ReusableData.MovementInput == Vector2.zero || StateMachine.ReusableData.MovementSpeedModifier == 0f)
            {
                Rigidbody.velocity = Vector3.zero;
                return;
            }

            var movementDirection = new Vector3(StateMachine.ReusableData.MovementInput.x, 0, StateMachine.ReusableData.MovementInput.y);
            var targetRotationYAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            var movementSpeed = GroundedData.BaseSpeed * StateMachine.ReusableData.MovementSpeedModifier;
            var currentHorizontalVelocity = GetHorizontalVelocity();

            Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction)
        {
            var directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTarget();

            return directionAngle;
        }

        private void UpdateData(float directionAngle)
        {
            StateMachine.ReusableData.CurrentTargetRotation.y = directionAngle;
            StateMachine.ReusableData.DampedPassedTime.y = 0f;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected float UpdateTargetRotation(Vector3 direction, bool isConsiderCameraRotation = true)
        {
            var directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (isConsiderCameraRotation)
            {
                if (directionAngle < 0f)
                {
                    directionAngle += 360f;
                }

                directionAngle += View.CameraTransform.eulerAngles.y;

                if (directionAngle < 360f)
                {
                    directionAngle -= 360f;
                }
            }

            if (!directionAngle.Equals(StateMachine.ReusableData.CurrentTargetRotation.y))
            {
                UpdateData(directionAngle);
            }

            return directionAngle;
        }

        protected void RotateTowardsTarget()
        {
            var currentYAngle = Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle.Equals(StateMachine.ReusableData.CurrentTargetRotation.y)) return;

            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, StateMachine.ReusableData.CurrentTargetRotation.y, ref StateMachine.ReusableData.DampedCurrentVelocity.y, StateMachine.ReusableData.TimeToReachTargetRotation.y - StateMachine.ReusableData.DampedPassedTime.y);

            StateMachine.ReusableData.DampedPassedTime.y += Time.deltaTime;

            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetHorizontalVelocity()
        {
            var horizontalVelocity = Rigidbody.velocity;
            horizontalVelocity.y = 0f;
            return horizontalVelocity;
        }

        protected void ResetVelocity()
        {
            Rigidbody.velocity = Vector3.zero;
        }
    }
}