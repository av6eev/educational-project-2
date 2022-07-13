using InputManager;
using Player.Data.States;
using Player.StateMachine.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using IState = Player.StateMachine.Utilities.IState;

namespace Player.StateMachine.Movement.States.Moving
{
    public class MovementState : IState
    {
        protected readonly Rigidbody Rigidbody;
        protected readonly GameContext Context;
        protected readonly PlayerView View;

        protected InputAction ToggleButton;
        protected InputAction RunButton;
        protected InputAction DashButton;

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
            DashButton = Context.InputModel.InputActions[InputActionsConstants.Dash];

            ToggleButton.started += OnToggle;
            RunButton.started += OnRun;
            RunButton.canceled += OnRun;
            DashButton.started += OnDash;
        }

        private void RemoveInputCallbacks()
        {
            ToggleButton.started -= OnToggle;
            RunButton.started -= OnRun;
            RunButton.canceled -= OnRun;
            DashButton.started -= OnDash;
        }

        protected virtual void OnDash(InputAction.CallbackContext ctx)
        {
            StateMachine.ChangeState(StateMachine.DashingState);
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

        public virtual void OnAnimationEnter()
        {
        }

        public virtual void OnAnimationExit()
        {
        }

        public virtual void OnAnimationTransition()
        {
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
            var movementSpeed = GroundedData.BaseSpeed * StateMachine.ReusableData.MovementSpeedModifier * StateMachine.ReusableData.OnSlopeSpeedModifier;
            var currentHorizontalVelocity = GetHorizontalVelocity();

            Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction)
        {
            var directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTarget(Time.deltaTime);

            return directionAngle;
        }
        
        protected float UpdateTargetRotation(Vector3 direction, bool isConsiderCameraRotation = true)
        {
            var directionAngle = GetDirectionAngle(direction);

            if (isConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (!directionAngle.Equals(StateMachine.ReusableData.CurrentTargetRotation.y))
            {
                UpdateData(directionAngle);
            }

            return directionAngle;
        }

        protected void RotateTowardsTarget(float deltaTime)
        {
            var currentYAngle = Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle.Equals(StateMachine.ReusableData.CurrentTargetRotation.y)) return;

            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, 
                StateMachine.ReusableData.CurrentTargetRotation.y,
                ref StateMachine.ReusableData.DampedCurrentVelocity.y,
                StateMachine.ReusableData.TimeToReachTargetRotation.y - StateMachine.ReusableData.DampedPassedTime.y);

            StateMachine.ReusableData.DampedPassedTime.y += deltaTime;

            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetHorizontalVelocity()
        {
            var horizontalVelocity = Rigidbody.velocity;
            horizontalVelocity.y = 0f;
            return horizontalVelocity;
        }

        protected Vector3 GetVerticalVelocity()
        {
            return new Vector3(0f, View.Rigidbody.velocity.y, 0f);
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        
        protected void ResetVelocity()
        {
            Rigidbody.velocity = Vector3.zero;
        }

        private void UpdateData(float directionAngle)
        {
            StateMachine.ReusableData.CurrentTargetRotation.y = directionAngle;
            StateMachine.ReusableData.DampedPassedTime.y = 0f;
        }
        
        private float AddCameraRotationToAngle(float angle)
        {
            angle += View.CameraTransform.eulerAngles.y;
            
            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            var directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        protected float GetMovementSpeed(bool isConsiderSlopes = true)
        {
            var movementSpeed = GroundedData.BaseSpeed * StateMachine.ReusableData.MovementSpeedModifier;
            
            if (isConsiderSlopes)
            {
                movementSpeed *= StateMachine.ReusableData.OnSlopeSpeedModifier;
            }

            return movementSpeed;
        }
    }
}