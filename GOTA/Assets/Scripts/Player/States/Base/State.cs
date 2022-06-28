using InputManager;
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
        protected float _playerSpeed;

        protected InputAction _moveAction;
        protected InputAction _cameraRotateAction;

        public StatesType Type;

        protected State(GameContext context, StatesEngine statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;
            
            _model = _context.PlayerModel;
            
            _moveAction = _context.InputModel.InputActions[InputActions.Movement];
            _cameraRotateAction = _context.InputModel.InputActions[InputActions.CameraRotate];
        }

        public abstract void Disable();

        public abstract void Enable();

        public virtual void HandleInput()
        {
            var cameraTransform = _context.GlobalContainer.PlayerView.CameraTransform;

            _moveAction.performed += ctx => _model.IsWalk = true; 
            _moveAction.canceled += ctx => _model.IsWalk = false;

            _model.IsRotationButtonEnable = _cameraRotateAction.IsPressed();
            
            _input = _moveAction.ReadValue<Vector2>();
            
            _velocity = new Vector3(_input.x, 0, _input.y);
            _context.PlayerModel.SetPosition(_velocity);
            
            _velocity = _velocity.x * cameraTransform.right.normalized + _velocity.z * cameraTransform.forward.normalized;
            _velocity.y = 0f;
        }

        public abstract void DoLogic();

        public abstract void DoPhysics(float deltaTime);
    }
}