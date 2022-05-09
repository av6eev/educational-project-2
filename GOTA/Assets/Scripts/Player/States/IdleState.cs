using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class IdleState : State
    {
        private readonly PlayerModel _model;
        private readonly PlayerData _data;
        
        private bool _isGrounded;
        private Vector3 _cVelocity;
        
        public IdleState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;
            
            _model = _context.PlayerModel;
            _data = _context.PlayerData;

            Type = StatesType.Idle;
        }

        public override void Disable()
        {
            _gravityVelocity.y = 0;
            _playerVelocity = new Vector3(_input.x, 0, _input.y);

            if (_velocity.sqrMagnitude > 0)
            {
                _context.GlobalContainer.PlayerView.transform.rotation = Quaternion.LookRotation(_velocity);
            }
        }
        
        public override void Enable()
        {
            Debug.Log("Idle state");
            _model.IsJump = false;
            _model.IsCrouch = false;
            _model.IsSprint = false;
            
            _input = Vector2.zero;
            _velocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
            _gravityVelocity.y = 0;

            _playerSpeed = _data.DefaultSpeed;
            _isGrounded = _context.GlobalContainer.PlayerView.CharacterController.isGrounded;
        }

        public override void HandleInput()
        {
            var cameraTransform = _context.GlobalContainer.PlayerView.CameraTransform;
            
            if (_jumpAction.triggered)
            {
                _model.IsJump = true;
            }
            //TODO: more

            _input = _moveAction.ReadValue<Vector2>();
            _velocity = new Vector3(_input.x, 0, _input.y);

            _velocity = _velocity.x * cameraTransform.right.normalized + _velocity.z * cameraTransform.forward.normalized;
            _velocity.y = 0f;

        }

        public override void DoLogic()
        {
            if (_model.IsJump)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Jump));
            }
            
            //TODO: more
        }

        public override void DoPhysics(float deltaTime)
        {
            var view = _context.GlobalContainer.PlayerView;
            var controller = view.CharacterController;
            var targetRotation = Quaternion.Euler(0, view.CameraTransform.eulerAngles.y, 0);
            
            view.CameraTransform = Camera.main.transform;
            
            _gravityVelocity.y = _data.Gravity * deltaTime;

            _isGrounded = controller.isGrounded;

            if (_isGrounded && _gravityVelocity.y < 0)
            {
                _gravityVelocity.y = 0f;
            }

            _currentVelocity = Vector3.SmoothDamp(_currentVelocity, _velocity, ref _cVelocity, _data.VelocityDampTime);
            controller.Move(_currentVelocity * deltaTime * _playerSpeed);

            if (_velocity.sqrMagnitude > 0)
            {
                view.transform.rotation = Quaternion.Lerp(view.transform.rotation, targetRotation, _data.RotationSpeed * deltaTime);
                // view.transform.rotation = Quaternion.Slerp(view.transform.rotation, Quaternion.LookRotation(_velocity), _data.RotationDampTime);
            }
        }
    }
}