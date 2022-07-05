using InputManager;
using Player.StateMachine.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.StateMachine.Movement.States
{
    public class MovementState : IState
    {
        protected readonly Rigidbody Rigidbody;
        protected readonly GameContext Context;
        protected readonly PlayerView View;

        protected Vector2 MovementInput;
        protected InputAction ToggleButton;
        protected InputAction RunButton;
        protected MovementStateMachine MovementStateMachine;

        private Vector3 _currentTargetRotation;
        private Vector3 _timeToReachTargetRotation;
        private Vector3 _dampedCurrentVelocity;
        private Vector3 _dampedPassedTime;

        public MovementState(MovementStateMachine movementStateMachine, GameContext context)
        {
            MovementStateMachine = movementStateMachine;

            Context = context;
            Rigidbody = Context.GlobalContainer.PlayerView.Rigidbody;
            View = Context.GlobalContainer.PlayerView;

            _timeToReachTargetRotation.y = 0.14f;
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
            MovementInput = Context.InputModel.InputActions[InputActionsConstants.Movement].ReadValue<Vector2>();
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
            Context.PlayerModel.ButtonToggled();
        }

        public virtual void LogicUpdate()
        {
            if (MovementInput == Vector2.zero || Context.PlayerData.SpeedModifier == 0f)
            {
                if (!Context.StateMachineEngine.GetStateMachine(StateMachineType.Movement).GetCurrentState().Equals(MovementStateMachine.IdlingState))
                {
                    MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);
                }
            }
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (MovementInput == Vector2.zero || Context.PlayerData.SpeedModifier == 0f)
            {
                Rigidbody.velocity = Vector3.zero;
                return;
            }

            var movementDirection = new Vector3(MovementInput.x, 0, MovementInput.y);
            var targetRotationYAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            var movementSpeed = Context.PlayerData.BaseSpeed * Context.PlayerData.SpeedModifier;
            var currentHorizontalVelocity = GetHorizontalVelocity();

            Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentHorizontalVelocity,
                ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction)
        {
            var directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTarget();

            return directionAngle;
        }

        private void UpdateData(float directionAngle)
        {
            _currentTargetRotation.y = directionAngle;
            _dampedPassedTime.y = 0f;
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

            if (!directionAngle.Equals(_currentTargetRotation.y))
            {
                UpdateData(directionAngle);
            }

            return directionAngle;
        }

        protected void RotateTowardsTarget()
        {
            var currentYAngle = Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle.Equals(_currentTargetRotation.y)) return;

            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, _currentTargetRotation.y,
                ref _dampedCurrentVelocity.y, _timeToReachTargetRotation.y - _dampedPassedTime.y);

            _dampedPassedTime.y += Time.deltaTime;

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