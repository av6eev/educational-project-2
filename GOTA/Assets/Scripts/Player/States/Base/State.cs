using InputManager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player.States.Base
{
    public abstract class State
    {
        protected StatesEngine StatesEngine;
        protected GameContext _context;
        private PlayerModel _model;

        protected Vector3 _velocity;
        protected Vector3 _currentVelocity;
        protected Vector3 _gravityVelocity;
        protected Vector2 _input;
        protected Vector3 _playerVelocity;
        private Vector3 _cVelocity;
        protected float _playerSpeed;

        protected InputAction _moveAction;
        protected InputAction _jumpAction;
        protected InputAction _sprintAction;

        public StatesType Type;

        protected State(GameContext context, StatesEngine statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;
            _model = _context.PlayerModel;

            _moveAction = _context.InputModel.InputActions[InputActions.Movement];
            _jumpAction = _context.InputModel.InputActions[InputActions.Jump];
            _sprintAction = _context.InputModel.InputActions[InputActions.Sprint];
        }

        public abstract void Disable();

        public abstract void Enable();

        public virtual void HandleInput()
        {
            var cameraTransform = _context.GlobalContainer.PlayerView.CameraTransform;

            _moveAction.performed += ctx => _model.IsWalk = true; 
            _moveAction.canceled += ctx => _model.IsWalk = false;
            _jumpAction.performed += ctx => _model.IsJump = ctx.ReadValueAsButton();
            _model.IsSprint = _sprintAction.IsPressed();
            
            _input = _moveAction.ReadValue<Vector2>();
            _velocity = new Vector3(_input.x, 0, _input.y);
            _context.PlayerModel.SetPosition(_velocity);
            _velocity = _velocity.x * cameraTransform.right.normalized + _velocity.z * cameraTransform.forward.normalized;
            _velocity.y = 0f;
        }

        public abstract void DoLogic();

        public virtual void DoPhysics(float deltaTime)
        {
            var view = _context.GlobalContainer.PlayerView;
            var controller = view.CharacterController;
            var targetRotation = Quaternion.Euler(0, view.CameraTransform.eulerAngles.y, 0);

            view.CameraTransform = Camera.main.transform;

            _gravityVelocity.y = _context.PlayerData.Gravity * deltaTime;

            _context.PlayerModel.IsGrounded = controller.isGrounded;

            if (_context.PlayerModel.IsGrounded && _gravityVelocity.y < 0)
            {
                _gravityVelocity.y = 0f;
            }

            _currentVelocity = Vector3.SmoothDamp(_currentVelocity, _velocity, ref _cVelocity, _context.PlayerData.VelocityDampTime);
            controller.Move(_currentVelocity * deltaTime * _playerSpeed);

            if (_velocity.sqrMagnitude > 0)
            {
                view.transform.rotation = Quaternion.Lerp(view.transform.rotation, targetRotation, _context.PlayerData.RotationSpeed * deltaTime);
            }
        }
    }
}