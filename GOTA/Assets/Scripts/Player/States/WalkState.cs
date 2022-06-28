using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class WalkState : State
    {
        private PlayerData _data;
        private PlayerModel _model;
        private PlayerView _view;
        
        private Vector3 _cVelocity;
        private Vector3 _positionToLook;

        public WalkState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _model = _context.PlayerModel;
            _view = _context.GlobalContainer.PlayerView;
            _data = _context.PlayerData;
            
            Type = StatesType.Walk;
        }

        public override void Disable()
        {
            _model.Walk(false);
        }

        public override void Enable()
        {
            Debug.Log("Walk state");

            _input = Vector2.zero;
            _velocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
            _gravityVelocity.y = 0;

            _playerSpeed = _data.DefaultSpeed;
            _model.IsGrounded = _context.GlobalContainer.PlayerView.CharacterController.isGrounded;
        }

        public override void DoLogic()
        {
            _model.Walk(_model.IsWalk);
            _context.GlobalContainer.PlayerView.SetAnimationSpeed(_input.magnitude, _data.SpeedDampTime, Time.deltaTime);
        }

        public override void DoPhysics(float deltaTime)
        {
            HandleMovement(deltaTime);
            HandleRotation(deltaTime);
        }

        private void HandleRotation(float deltaTime)
        {
            _positionToLook = new Vector3(_input.x, 0f, _input.y);

            var currentRotation = _view.transform.rotation;
            var targetRotation = Quaternion.LookRotation(_positionToLook);

            if (_model.IsWalk)
            {
                _view.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _data.RotationDampTime * deltaTime);
            }
        }

        private void HandleMovement(float deltaTime)
        {
            _gravityVelocity.y = _data.Gravity * deltaTime;
            _model.IsGrounded = _view.CharacterController.isGrounded;

            if (_model.IsGrounded && _gravityVelocity.y < 0)
            {
                _gravityVelocity.y = 0f;
            }

            _currentVelocity = Vector3.SmoothDamp(_currentVelocity, _velocity, ref _cVelocity, _data.VelocityDampTime);
            _view.CharacterController.Move(_currentVelocity * deltaTime * _playerSpeed);
        }
    }
}