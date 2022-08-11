using InputManager;
using Player.Data.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using IState = Player.StateMachine.Utilities.IState;

namespace Player.StateMachine.Movement.States
{
    public class MovementState : IState
    {
        private readonly Rigidbody _rigidbody;
        protected readonly GameContext Context;
        protected readonly PlayerView View;

        protected InputAction MoveAction;
        private InputAction _toggleButton;
        private InputAction _runStarted;
        private InputAction _runCanceled;
        private InputAction _dashButton;
        private InputAction _jumpAction;

        protected readonly MovementStateMachine StateMachine;
        protected readonly GroundedData GroundedData;
        protected readonly AirborneData AirborneData;

        public MovementState(MovementStateMachine stateMachine, GameContext context)
        {
            StateMachine = stateMachine;
            Context = context;

            GroundedData = Context.PlayerSO.GroundedData;
            AirborneData = Context.PlayerSO.AirborneData;
            
            _rigidbody = Context.GlobalContainer.PlayerView.Rigidbody;
            View = Context.GlobalContainer.PlayerView;

            Initialize();
        }

        private void Initialize()
        {
            SetRotationData();

            MoveAction = Context.InputModel.InputActions[InputActionsConstants.Movement];
            _toggleButton = Context.InputModel.InputActions[InputActionsConstants.ToggleButton];
            _dashButton = Context.InputModel.InputActions[InputActionsConstants.Dash];
            _runStarted = Context.InputModel.InputActions[InputActionsConstants.RunStart];
            _runCanceled = Context.InputModel.InputActions[InputActionsConstants.RunCancel];
            _jumpAction = Context.InputModel.InputActions[InputActionsConstants.Jump];
        }

        #region Default Methods

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

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
            Move();
            GroundCheck();
            
            Debug.Log(StateMachine.ReusableData.IsGrounded);
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
        #endregion
        
        protected virtual void AddInputCallbacks()
        {
            MoveAction.canceled += OnMoveCanceled;
            _toggleButton.started += OnToggle;
            _dashButton.started += OnDash;
            _runStarted.performed += ctx => StateMachine.ReusableData.IsRunning = true;
            _runCanceled.performed += ctx =>  StateMachine.ReusableData.IsRunning = false;
            _jumpAction.started += OnJump;
        }

        protected virtual void RemoveInputCallbacks()
        {
            MoveAction.canceled -= OnMoveCanceled;
            _toggleButton.started -= OnToggle;
            _dashButton.started -= OnDash;
            _jumpAction.started -= OnJump;
        }

        protected virtual void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
        }
        
        protected virtual void OnJump(InputAction.CallbackContext ctx)
        {
            if (StateMachine.ReusableData.IsGrounded)
            {
                StateMachine.ChangeState(StateMachine.JumpingState);
            }
        }
        
        protected virtual void OnDash(InputAction.CallbackContext ctx)
        {
            if (StateMachine.ReusableData.IsButtonToggled)
            {
                StateMachine.ChangeState(StateMachine.DashingState);
            }
        }

        private void OnToggle(InputAction.CallbackContext ctx)
        {
            StateMachine.ReusableData.IsButtonToggled = !StateMachine.ReusableData.IsButtonToggled;
        }

        private void Move()
        {
            if (StateMachine.ReusableData.MovementInput == Vector2.zero || StateMachine.ReusableData.MovementSpeedModifier == 0f)
            {
                return;
            }

            var movementDirection = new Vector3(StateMachine.ReusableData.MovementInput.x, 0, StateMachine.ReusableData.MovementInput.y);
            var targetRotationYAngle = Rotate(movementDirection);
            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            var movementSpeed = GroundedData.BaseSpeed * StateMachine.ReusableData.MovementSpeedModifier * StateMachine.ReusableData.OnSlopeSpeedModifier;
            var currentHorizontalVelocity = GetHorizontalVelocity();

            _rigidbody.AddForce(targetRotationDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);
        }
        
        private void GroundCheck()
        {
            var worldColliderCenter = View.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
            var ray = new Ray(worldColliderCenter, Vector3.down);

            StateMachine.ReusableData.IsGrounded = Physics.Raycast(ray, View.ColliderUtility.SlopeData.RayDistance, View.LayerData.GroundLayer);
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

        #region Reusable Methods
        
        protected float GetMovementSpeed(bool isConsiderSlopes = true)
        {
            var movementSpeed = GroundedData.BaseSpeed * StateMachine.ReusableData.MovementSpeedModifier;
            
            if (isConsiderSlopes)
            {
                movementSpeed *= StateMachine.ReusableData.OnSlopeSpeedModifier;
            }

            return movementSpeed;
        }

        protected void DecelerateHorizontally()
        {
            var horizontalVelocity = GetHorizontalVelocity();
            View.Rigidbody.AddForce(-horizontalVelocity * StateMachine.ReusableData.DecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minMagnitude = 0.1f)
        {
            var horizontalVelocity = GetHorizontalVelocity();
            var horizontalMovement = new Vector2(horizontalVelocity.x, horizontalVelocity.z);
            return horizontalMovement.magnitude > minMagnitude;
        }
        
        protected void SetRotationData()
        {
            StateMachine.ReusableData.RotationData = GroundedData.RotationData;
            StateMachine.ReusableData.TimeToReachTargetRotation = StateMachine.ReusableData.RotationData.TargetRotationReachTime;
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

        protected void RotateTowardsTarget()
        {
            var currentYAngle = _rigidbody.rotation.eulerAngles.y;
            if (currentYAngle.Equals(StateMachine.ReusableData.CurrentTargetRotation.y)) return;

            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, 
                StateMachine.ReusableData.CurrentTargetRotation.y,
                ref StateMachine.ReusableData.DampedCurrentVelocity.y,
                StateMachine.ReusableData.TimeToReachTargetRotation.y - StateMachine.ReusableData.DampedPassedTime.y);

            StateMachine.ReusableData.DampedPassedTime.y += Time.deltaTime;

            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            _rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetHorizontalVelocity()
        {
            var horizontalVelocity = _rigidbody.velocity;
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
            _rigidbody.velocity = Vector3.zero;
        }
        #endregion
    }
}