using Player.States.Base;
using UnityEngine;
using Utilities;

namespace Player.States
{
    public class JumpState : State
    {
        private readonly PlayerModel _model;
        private readonly PlayerData _data;

        public JumpState(GameContext context, StatesEngine statesEngine) : base(context, statesEngine)
        {
            _context = context;
            StatesEngine = statesEngine;

            _model = _context.PlayerModel;
            _data = _context.PlayerData;

            Type = StatesType.Jump;
        }

        public override void Disable()
        {
            _model.Jump(false);
        }

        public override void Enable()
        {
            _model.Jump(true);
            Debug.Log("Jump state");

            _model.IsGrounded = false;
            _gravityVelocity.y = 0;

            Jump();
        }

        public override void DoLogic()
        {
            if (_model.IsGrounded)
            {
                StatesEngine.Change(StatesEngine.Get(StatesType.Land));
            }
        }

        public override void DoPhysics(float deltaTime)
        {
            if (!_model.IsGrounded)
            {
                var airVelocity = new Vector3(_input.x, 0, _input.y);
                var view = _context.GlobalContainer.PlayerView;
                var transformRight = view.CameraTransform.right;
                var transformForward = view.CameraTransform.forward;

                _velocity = _playerVelocity;
                _velocity.y = 0f;
                _velocity = _velocity.x * transformRight.normalized + _velocity.z * transformForward.normalized;

                airVelocity = airVelocity.x * transformRight.normalized + airVelocity.z * transformForward.normalized;
                airVelocity.y = 0f;

                view.CharacterController.Move(_gravityVelocity * deltaTime + (airVelocity * _data.AirControl + _velocity * (1 - _data.AirControl)) * _playerSpeed * deltaTime);

                _gravityVelocity.y += _data.Gravity * deltaTime;
                _model.IsGrounded = view.CharacterController.isGrounded;
            }
        }

        private void Jump()
        {
            _gravityVelocity.y += Mathf.Sqrt(_data.JumpHeight * -3.0f * _data.Gravity);
        }
    }
}